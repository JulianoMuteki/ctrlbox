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
using Newtonsoft.Json;

namespace CtrlBox.UI.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IClientApplicationService _clientAppService;
        private readonly IProductApplicationService _productAppService;
        private readonly ITrackingApplicationService _trackingAppService;

        public ProductController(NotificationContext notificationContext, IClientApplicationService clientService, IProductApplicationService productService, ITrackingApplicationService trackingService)
            : base(notificationContext)
        {
            _clientAppService = clientService;
            _productAppService = productService;
            _trackingAppService = trackingService;
        }

        #region Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerGet()
        {
            var productVMs = _productAppService.GetAll();
            return Json(new
            {
                aaData = productVMs,
                success = true
            });
        }

        public ActionResult Create(Guid productID)
        {
            var productVM = _productAppService.GetById(productID);

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
                    _productAppService.Add(productVM);
                else
                    _productAppService.Update(productVM);

                if (_notificationContext.HasNotifications)
                {
                    ViewData["Notifications"] = _notificationContext.Notifications.ToList();
                    return View();
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
            _productAppService.Update(productVM);

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
            var clientsVM = _clientAppService.GetById(new Guid(clientID));
            return View(clientsVM);
        }

        public ActionResult AjaxHandlerProductValue(string clientID)
        {
            try
            {
                var productsClientsVMs = _productAppService.GetClientsProductsByClientID(new Guid(clientID));
                var productVMs = _productAppService.GetAll();

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

            _productAppService.ConnectRouteToClient(clientsProductsValuesVM);

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
            LoadViewDataClients(_clientAppService);
            LoadViewDataProducts(_productAppService);
            LoadViewDataTracking(_trackingAppService);

            return View();
        }

        public ActionResult GetTotalProductItemByProductID(Guid productID)
        {
            try
            {
                var totalProductsItems = _productAppService.GetTotalProductItemByProductID(productID);
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
                _productAppService.AddStockProduct(productID, clientID, trackingTypeID, quantity);
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
            LoadViewDataProducts(_productAppService);

            return View();
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerProductItem()
        {
            try
            {
                var productsItems = _productAppService.GetProductsItems();
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
            LoadViewDataProducts(_productAppService);

            return View();
        }

        [HttpPost]
        public ActionResult PutAjaxHandlerProductItem(Guid productID, int quantity)
        {
            JsonSerialize jsonS = new JsonSerialize();
            _productAppService.GenerateProductItem(productID, quantity);

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
            var stocks = _productAppService.GetStocks();
            return Json(new
            {
                aaData = stocks,
                success = true
            });
        }

        public IActionResult StockCreate()
        {
            LoadViewDataClients(_clientAppService);
            LoadViewDataProducts(_productAppService);
            return View();
        }

        [HttpPost]
        public IActionResult StockCreate(StockVM entityVM)
        {
            _productAppService.AddStock(entityVM);
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
            var stocksMovements = _productAppService.GetstocksMovements(stockID);
            return Json(new
            {
                aaData = stocksMovements,
                success = true
            });
        }

        public IActionResult StockMovementsCreate()
        {
            LoadViewDataClients(_clientAppService);
            LoadViewDataProducts(_productAppService);
            return View();
        }

        [HttpPost]
        public IActionResult StockMovementsCreate(StockMovementVM entityVM)
        {
            var stockID = _productAppService.AddStockMovement(entityVM);
            return RedirectToAction("StocksMovements", stockID);
        }
    }
}