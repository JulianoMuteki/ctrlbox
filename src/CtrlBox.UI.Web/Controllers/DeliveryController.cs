using System;
using System.Collections.Generic;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CtrlBox.UI.Web.Extensions;
using CtrlBox.Domain.Security;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CtrlBox.UI.Web.Controllers
{
    [AuthorizeEnum(RoleAuthorize.Admin, RoleAuthorize.Manager, RoleAuthorize.Delivery)]
    public class DeliveryController : Controller
    {
        private readonly IClientApplicationService _clientService;
        private readonly IDeliveryApplicationService _deliveryService;
        private readonly IRouteApplicationService _routeService;
        private readonly IProductApplicationService _productService;
        private readonly ISaleApplicationService _saleService;
        private readonly ISecurityApplicationService _securityService;
        private readonly IBoxApplicationService _boxService;
        private readonly ITrackingApplicationService _trackingApplicationService;

        public DeliveryController(IClientApplicationService clientService, IRouteApplicationService routeService,
                                   IDeliveryApplicationService deliveryService, IProductApplicationService productService,
                                   ISaleApplicationService saleService, ISecurityApplicationService securityService,
                                   IBoxApplicationService boxService, ITrackingApplicationService trackingApplicationService)
        {
            _boxService = boxService;
            _clientService = clientService;
            _routeService = routeService;
            _deliveryService = deliveryService;
            _productService = productService;
            _saleService = saleService;
            _securityService = securityService;
            _trackingApplicationService = trackingApplicationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetTableAjaxHandlerDeliveries()
        {
            try
            {
                ICollection<OrderVM> deliveriesVM;

                if (User.IsInRole(RoleAuthorize.Driver.ToString()))
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    deliveriesVM = _deliveryService.GetByUserId(new Guid(userId));
                }
                else
                {
                    deliveriesVM = _deliveryService.GetAll();
                }

                return Json(new
                {
                    aaData = deliveriesVM,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Create()
        {
            var routes = _routeService.GetAll()
                               .Select(route => new SelectListItem
                               {
                                   Value = route.DT_RowId,
                                   Text = route.Name
                               }).ToList();
            ViewData["Routes"] = routes;

            var users = _securityService.GetAllUsers()
                                 .Select(user => new SelectListItem
                                 {
                                     Value = user.Id.ToString(),
                                     Text = $"{user.FirstName} - {user.LastName}"
                                 }).ToList();
            ViewData["Users"] = users;

            return View();
        }

        [HttpGet]
        public ActionResult GetBoxesByRouteID(Guid routeID)
        {
            try
            {
                var boxesVM = _boxService.GetBoxesStockParents(routeID);
                var boxes = boxesVM.GroupBy(n => n.BoxTypeID)
                    .Select(g => new
                    {
                        DT_RowId = g.Key,
                        BoxType = g.Select(x => x.BoxType.Name).FirstOrDefault(),
                        SrcPicture = g.Select(x => x.BoxType.Picture.SrcBase64Image).FirstOrDefault(),
                        TotalBox = g.Count()
                    }
                    ).ToList();


                return Json(new
                {
                    aaData = boxes,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult GetAjaxHandlerUsers()
        {
            var usersList = _securityService.GetAllUsers()
                                                   .Select(user => new SelectListItem
                                                   {
                                                       Value = user.Id.ToString(),
                                                       Text = user.UserName
                                                   }).ToList();

            return Json(new
            {
                aaData = usersList,
                success = true
            });
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerProductsStock()
        {
            var productVMs = _productService.GetAll();

            return Json(new
            {
                aaData = productVMs,
                success = true
            });
        }

        [HttpPost]
        public ActionResult PostAjaxHandlerCreateDelivery(string[] tbBoxesTypes, string routeID, string userID)
        {
            try
            {
                JsonSerialize jsonS = new JsonSerialize();
                var deliveryBoxTypeVMs = jsonS.JsonDeserialize<BoxTypeVM>(tbBoxesTypes[0]);

                OrderVM deliveryVM = new OrderVM();
                deliveryVM.RouteID = new Guid(routeID);
                deliveryVM.UserID = new Guid(userID);
                deliveryVM.BoxesTypes = deliveryBoxTypeVMs;

                _deliveryService.Add(deliveryVM);
                return Json(new
                {
                    success = true,
                    Message = "OK"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[AuthorizePolicyEnum(CRUD.Create)]
        public ActionResult ViewDelivery(string deliveryID, string routeID)
        {
            ViewData["DeliveryID"] = deliveryID;
            ViewData["RouteID"] = routeID;

            ViewData["RouteName"] = _routeService.GetById(new Guid(routeID)).Name;
            return View();
        }

        public ActionResult GetAjaxHandlerViewDelivery(string deliveryID)
        {
            try
            {
                Guid id = new Guid(deliveryID);
                var deliveryVM = _deliveryService.GetById(id);
                var clientsVM = _clientService.GetByRouteIDAndOrderID(deliveryVM.RouteID, id);
                ICollection<ExpenseVM> despesasVM = new List<ExpenseVM>();

                var boxesLoadInRoute = _boxService.GetBoxesByDeliveryID(id).GroupBy(n => n.BoxTypeID)
                  .Select(g => new
                  {
                      DT_RowId = g.Key,
                      BoxType = g.Select(x => x.BoxType.Name).FirstOrDefault(),
                      PictureID = g.Select(x => x.BoxType.PictureID).FirstOrDefault(),
                      TotalBox = g.Count(),
                      TotalProductItems = g.Where(x=>x.ProductID != null).Sum(x => x.TotalProductsItemsChildren)
                  }
                  ).ToList();

                return Json(new
                {
                    aaData = clientsVM,
                    xaData = boxesLoadInRoute.OrderBy(x=>x.TotalProductItems).ToList(),
                    xbData = despesasVM,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SubmitDespesa(string entregaID, string descricao, string valor)
        {
            Guid idEntrega = new Guid(entregaID);

            ExpenseVM despesa = new ExpenseVM()
            {
                //DespesaID = Guid.NewGuid(),
                //EntregaID = idEntrega,
                //Descricao = descricao,
                //Valor = Convert.ToDouble(valor)
            };
            //context.Despesas.Add(despesa);
            //context.SaveChanges();

            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        public ActionResult PutAjaxHandlerFinalizeDelivery(Guid deliveryID)
        {
            try
            {
                _deliveryService.FinalizeDelivery(deliveryID);

                return Json(new
                {
                    aaData = "ok",
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult GetTableAjaxHandlerResumeDelivery(Guid deliveryID)
        {
            try
            {
                var deliveryVM = _deliveryService.GetResumeDeliveryById(deliveryID);

                return Json(new
                {
                    aaData = new
                    {
                        TotalSale = deliveryVM.Sales.Sum(x => x.SalesProducts.Sum(s => s.TotalValue)),
                        TotalProducts = deliveryVM.Sales.Sum(x => x.SalesProducts.Sum(p => p.Quantity)),
                        StartDate = deliveryVM.DtStart.ToString("dd/MM/yyyy hh:mm")
                    },

                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult MakeDelivery(Guid routeID, Guid clientID, Guid deliveryID)
        {
            var trackingsTypes = _trackingApplicationService.GetAllTrackingsTypesByPlace()
                            .Select(trace => new SelectListItem
                            {
                                Value = trace.DT_RowId,
                                Text = trace.Description
                            }).ToList();
            ViewData["TrackingTypes"] = trackingsTypes;


            var client = _clientService.GetById(clientID);
            ViewData["Client"] = client.Name;
            ViewData["ClientID"] = clientID;
            ViewData["DeliveryID"] = deliveryID;

            return View();
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerMakeDelivery(Guid clientID, Guid deliveryID)
        {
            try
            {
                //Busca preço de produtos por clientes. Deve sempre existir preço para todos clientes
                var boxesProductItemsVM = _boxService.GetOrderProductItemByDeliveryID(deliveryID);

                var orderProductItemsGroup = boxesProductItemsVM.GroupBy(item => item.ProductItem.Product.DT_RowId,
                                                                  (key, group) => new {
                                                                      DT_RowId = key,
                                                                      Product = group.Select(x => x.ProductItem.Product).FirstOrDefault(),
                                                                      NomeProduto = group.Select(x => x.ProductItem.Product.Name).FirstOrDefault(),
                                                                      PictureID = group.Select(x => x.ProductItem.Product.PictureID).FirstOrDefault(),
                                                                      TotalBox = group.Select(p => p.ProductItem).Count()
                                                                  })
                                                         .ToList();

                return Json(new
                {
                    aaData = orderProductItemsGroup.Select(x => new
                    {
                        DT_RowId = x.DT_RowId.ToString(),
                        x.NomeProduto,
                        Product = new
                        {
                            x.Product.Description,
                            x.Product.Package,
                            Capacity = $"{x.Product.Capacity} {x.Product.UnitMeasure}",
                            Weight = $"{x.Product.Weight} {x.Product.MassUnitWeight}"
                        },
                        x.PictureID,
                        x.TotalBox,
                        Total = String.Format("{0:c}", 0)
                    }),
                    success = true,
                    SaldoAnterior = 0,
                    CaixasEmDebito = 0
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult PostAjaxHandlerMakeDelivery(string[] strMakeDeliveryJSON, Guid trackingTypeID)
        {
            try
            {
                JsonSerialize jsonS = new JsonSerialize();
                var deliveryVM = jsonS.JsonDeserializeObject<OrderVM>(strMakeDeliveryJSON[0]);
                _deliveryService.MakeDelivery(deliveryVM, trackingTypeID);

                return Json(new
                {
                    success = true,
                    Message = "OK"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult PostAjaxHanblerFinishDelivery(Guid orderID, bool hasCrossDocking)
        {
            try
            {            
                _deliveryService.FinishDelivery(orderID);

                return Json(new
                {
                    success = true,
                    Message = "OK"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}