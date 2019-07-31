using System;
using System.Collections.Generic;
using System.Linq;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CtrlBox.UI.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IClientApplicationService _clientService;
        private readonly IProductApplicationService _productService;

        public ProductController(IClientApplicationService clientService, IProductApplicationService productService)
        {
            _clientService = clientService;
            _productService = productService;
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
            PictureVM imageEntity = GeneratePicture.CreatePicture(FilePicture, $"{productVM.Name} - {productVM.Description}");
            productVM.Picture = imageEntity;

            if (string.IsNullOrEmpty(productVM.DT_RowId))
                _productService.Add(productVM);
            else
                _productService.Update(productVM);

            return RedirectToAction("Index");
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
            return View();
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



    }
}