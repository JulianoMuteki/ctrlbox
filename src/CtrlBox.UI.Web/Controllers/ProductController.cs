﻿using System;
using System.Collections.Generic;
using System.Linq;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
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
            return View(productVM);
        }

        [HttpPost]
        public ActionResult Create(ProductVM productVM)
        {
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
            var stockVM = _productService.GetStock();
            ViewData["StockID"] = stockVM.DT_RowId;
            return View();
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerProductStock()
        {
            try
            {
                var productsStocks = _productService.GetProductsStock();
                return Json(new
                {
                    aaData = productsStocks.Select(x => new { x.StockID, x.ProductID, x.Amount, ProductName = x.Product.Name, x.Product.UnitMeasure }).ToList(),
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult PutAjaxHandlerProductStock(string[] tbProducts)
        {
            JsonSerialize jsonS = new JsonSerialize();
            var stocksProductsVM = jsonS.JsonDeserialize<StockProductVM>(tbProducts[0]);
            var result = _productService.AddProductStock(stocksProductsVM);

            return Json(new
            {
                success = true,
                Message = "OK"
            });
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
                                   Text = $"{prod.Name} {prod.UnitMeasure}"
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