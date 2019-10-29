using System;
using System.Collections.Generic;
using System.Linq;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Extensions;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CtrlBox.UI.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IClientApplicationService _clientService;
        private readonly IProductApplicationService _productService;
        private readonly ITrackingApplicationService _trackingService;

        public ProductController(NotificationContext notificationContext, IClientApplicationService clientService, IProductApplicationService productService, ITrackingApplicationService trackingService)
            : base(notificationContext)
        {
            _clientService = clientService;
            _productService = productService;
            _trackingService = trackingService;
        }

        #region Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerGet()
        {
            var productVMs = _productService.GetAll();
            return Json(new
            {
                aaData = productVMs,
                success = true
            });
        }

        public ActionResult Create(Guid productID)
        {
            var productVM = _productService.GetById(productID);

            var optionsBoxMassUnit = CtrlBoxUnits.CtrlBoxMassUnit
                                        .Select(unit => new SelectListItem
                                        {
                                            Value = unit,
                                            Text = unit
                                        }).ToList();
            ViewData["OptionsBoxMassUnit"] = optionsBoxMassUnit;

            var optionsBoxVolumeUnit = CtrlBoxUnits.CtrlBoxVolumeUnit
                                        .Select(unit => new SelectListItem
                                        {
                                            Value = unit,
                                            Text = unit
                                        }).ToList();
            ViewData["OptionsBoxVolumeUnit"] = optionsBoxVolumeUnit;

            var optionsBoxUnitType = CtrlBoxUnits.CtrlBoxUnitType
                            .Select(unit => new SelectListItem
                            {
                                Value = unit,
                                Text = unit
                            }).ToList();
            ViewData["OptionsBoxUnitType"] = optionsBoxUnitType;

            return View(productVM);
        }

        [HttpPost]
        public ActionResult Create(ProductVM productVM, IFormFile FilePicture)
        {
            try
            {
                PictureVM imageEntity = GeneratePicture.CreatePicture(FilePicture, $"{productVM.Name} - {productVM.Description}");
                productVM.Picture = imageEntity;

                if (string.IsNullOrEmpty(productVM.DT_RowId))
                    _productService.Add(productVM);
                else
                    _productService.Update(productVM);

                if (_notificationContext.HasNotifications)
                {
                    base.PushNotification();
                    return View(productVM);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductVM productVM)
        {
            _productService.Update(productVM);

            return Json(new
            {
                success = true,
                Message = "OK"
            });
        }
        #endregion

        #region ClientProductValue
        public ActionResult ClientProductValue(string clientID)
        {
            var clientsVM = _clientService.GetById(new Guid(clientID));
            return View(clientsVM);
        }

        public ActionResult AjaxHandlerProductValue(string clientID)
        {
            try
            {
                var productsClientsVMs = _productService.GetClientsProductsByClientID(new Guid(clientID));
                var productVMs = _productService.GetAll();

                IList<ClientProductValueVM> valoresProdutos = new List<ClientProductValueVM>();
                foreach (var product in productVMs)
                {
                    var prodValueExits = productsClientsVMs.Where(x => x.ProductID.ToString() == product.DT_RowId).FirstOrDefault();

                    valoresProdutos.Add(new ClientProductValueVM() { Product = product, ClientID = new Guid(clientID), Price = (prodValueExits != null ? prodValueExits.Price : 0) });
                }
                return Json(new
                {
                    aaData = valoresProdutos
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SubmitProductValue(string[] listJSON)
        {
            JsonSerialize jsonS = new JsonSerialize();
            var clientsProductsValuesVM = jsonS.JsonDeserialize<ClientProductValueVM>(listJSON[0]);

            _productService.ConnectRouteToClient(clientsProductsValuesVM);

            return Json(new
            {
                success = true,
                Message = "OK"
            });
        }
        #endregion

        #region ProductStock
        public ActionResult ProductStock()
        {
            var clients = _clientService.GetAll()
                      .Select(client => new SelectListItem
                      {
                          Value = client.DT_RowId,
                          Text = client.Name
                      }).ToList();
            ViewData["Clients"] = clients;

            var products = _productService.GetAll()
                            .Select(prod => new SelectListItem
                            {
                                Value = prod.DT_RowId,
                                Text = $"{prod.Name} - {prod.Description} - {prod.Package} - {prod.Capacity}{prod.UnitMeasure}"
                            }).ToList();
            ViewData["Products"] = products;

            var trackingsTypes = _trackingService.GetAllTrackingsTypesByPlace()
                .Select(trace => new SelectListItem
                {
                    Value = trace.DT_RowId,
                    Text = trace.Description
                }).ToList();
            ViewData["TrackingTypes"] = trackingsTypes;

            return View();
        }

        public ActionResult GetTotalProductItemByProductID(Guid productID)
        {
            try
            {
                var totalProductsItems = _productService.GetTotalProductItemByProductID(productID);
                return Json(new
                {
                    aaData = totalProductsItems,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult PostAjaxHandlerAddStockProduct(Guid productID, Guid clientID, Guid trackingTypeID, int quantity)
        {
            try
            {
                _productService.AddStockProduct(productID, clientID, trackingTypeID, quantity);
                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public ActionResult ProductItem()
        {
            var products = _productService.GetAll()
                               .Select(prod => new SelectListItem
                               {
                                   Value = prod.DT_RowId,
                                   Text = $"{prod.Name} {prod.UnitMeasure}"
                               }).ToList();
            ViewData["Products"] = products;

            return View();
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerProductItem()
        {
            try
            {
                var productsItems = _productService.GetProductsItems();
                return Json(new
                {
                    aaData = productsItems,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GenerateProductItem()
        {
            var products = _productService.GetAll()
                               .Select(prod => new SelectListItem
                               {
                                   Value = prod.DT_RowId,
                                   Text = prod.FormattedProduct
                               }).ToList();
            ViewData["Products"] = products;

            return View();
        }

        [HttpPost]
        public ActionResult PutAjaxHandlerProductItem(Guid productID, int quantity)
        {
            JsonSerialize jsonS = new JsonSerialize();
            _productService.GenerateProductItem(productID, quantity);

            return Json(new
            {
                success = true,
                Message = "OK"
            });
        }

        public ActionResult Stock()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerStocks()
        {
            var stocks = _productService.GetStocks();
            return Json(new
            {
                aaData = stocks,
                success = true
            });
        }

        public IActionResult StockCreate()
        {
            var products = _productService.GetAll()
                            .Select(prod => new SelectListItem
                            {
                                Value = prod.DT_RowId,
                                Text = $"{prod.Name} - {prod.Description} - {prod.Package} - {prod.Capacity}{prod.UnitMeasure}"
                            }).ToList();
            ViewData["Products"] = products;

            var clients = _clientService.GetAll()
                                        .Select(client => new SelectListItem
                                        {
                                            Value = client.DT_RowId,
                                            Text = client.Name
                                        }).ToList();
            ViewData["Clients"] = clients;
            return View();
        }

        [HttpPost]
        public IActionResult StockCreate(StockVM entityVM)
        {
            _productService.AddStock(entityVM);
            return RedirectToAction("Stock");
        }

        public IActionResult StocksMovements(Guid stockID)
        {
            ViewData["StockID"] = stockID;
            return View();
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerStocksMovements(Guid stockID)
        {
            var stocksMovements = _productService.GetstocksMovements(stockID);
            return Json(new
            {
                aaData = stocksMovements,
                success = true
            });
        }

        public IActionResult StockMovementsCreate()
        {
            var products = _productService.GetAll()
                            .Select(prod => new SelectListItem
                            {
                                Value = prod.DT_RowId,
                                Text = $"{prod.Name} - {prod.Description} - {prod.Package} - {prod.Capacity}{prod.UnitMeasure}"
                            }).ToList();
            ViewData["Products"] = products;

            var clients = _clientService.GetAll()
                                        .Select(client => new SelectListItem
                                        {
                                            Value = client.DT_RowId,
                                            Text = client.Name
                                        }).ToList();
            ViewData["Clients"] = clients;
            return View();
        }

        [HttpPost]
        public IActionResult StockMovementsCreate(StockMovementVM entityVM)
        {
            var stockID = _productService.AddStockMovement(entityVM);
            return RedirectToAction("StocksMovements", stockID);
        }
    }
}