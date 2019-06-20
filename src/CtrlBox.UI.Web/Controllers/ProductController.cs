using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductVM productVM)
        {
            _productService.Add(productVM);
            var productVMs = _productService.GetAll();
            return View("Index", productVMs);
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
                    aaData = productsStocks.Select(x => new { x.StockID, x.ProductID, x.Amount, ProductName = x.Product.Name }).ToList(),
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
    }
}