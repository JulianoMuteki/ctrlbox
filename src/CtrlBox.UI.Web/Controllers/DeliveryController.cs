﻿using System;
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

        public DeliveryController(IClientApplicationService clientService, IRouteApplicationService routeService,
                                   IDeliveryApplicationService deliveryService, IProductApplicationService productService,
                                   ISaleApplicationService saleService, ISecurityApplicationService securityService,
                                   IBoxApplicationService boxService)
        {
            _boxService = boxService;
            _clientService = clientService;
            _routeService = routeService;
            _deliveryService = deliveryService;
            _productService = productService;
            _saleService = saleService;
            _securityService = securityService;
        }

        public ActionResult Index()
        {
            ViewBag.Stock = "Check stock of the product: XXXXXXXX";
            return View();
        }

        [HttpGet]
        public ActionResult GetTableAjaxHandlerDeliveries()
        {
            try
            {
                ICollection<DeliveryVM> deliveriesVM;

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
            return View();
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

                DeliveryVM deliveryVM = new DeliveryVM();
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

        [AuthorizePolicyEnum(CRUD.Create)]
        public ActionResult ExecuteDelivery(string entregaID, string linhaID)
        {
            ViewData["entregaID"] = entregaID;
            ViewData["linhaID"] = linhaID;

            ViewData["RouteName"] = _routeService.GetById(new Guid(linhaID)).Name;
            return View();
        }

        public ActionResult AjaxHandlerExecutarEntrega(string entregaID)
        {
            try
            {
                Guid deliveryID = new Guid(entregaID);
                var deliveryVM = _deliveryService.GetById(deliveryID);
                var routeVM = _routeService.GetById(deliveryVM.RouteID);
                var clientsVM = _clientService.GetByRouteID(new Guid(routeVM.DT_RowId));
               
                ICollection<ExpenseVM> despesasVM = new List<ExpenseVM>();
                var sales = _saleService.FindAllByDelivery(new Guid(deliveryVM.DT_RowId));
                var clientsVMs = clientsVM.Select(c =>
                                            {
                                                c.SaleVM =
                                                       ((from x in sales
                                                         where x.ClientID.ToString() == c.DT_RowId
                                                         select x).FirstOrDefault() ?? new SaleVM()); return c;
                                            }).ToList();


                var boxesLoadInRoute = _boxService.GetBoxesByDeliveryID(deliveryID).GroupBy(n => new { n.BoxTypeID, n })
                                                  .Select(g => new
                                                  {
                                                      DT_RowId = g.Key.BoxTypeID,
                                                      g.Key.n.BoxType.PictureID,
                                                      BoxType = g.Key.n.BoxType.Name,
                                                      TotalBox = g.Count(),
                                                      TotalProductItems = g.Sum(x=>x.TotalProductsItemsChildren)
                                                  });

                return Json(new
                {
                    aaData = clientsVM,
                    xaData = boxesLoadInRoute.ToList(),
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
                    aaData = new { TotalSale = deliveryVM.Sales.Sum(x => x.SalesProducts.Sum(s => s.TotalValue)),
                        TotalProducts = deliveryVM.Sales.Sum(x => x.SalesProducts.Sum(p => p.Quantity)),
                        StartDate = deliveryVM.DtStart.ToString("dd/MM/yyyy hh:mm")},

                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}