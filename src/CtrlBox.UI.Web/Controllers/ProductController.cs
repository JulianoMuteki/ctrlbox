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
        private readonly IMapper _mapper;

        public ProductController(IClientApplicationService clientService, IProductApplicationService productService, IMapper mapper)
        {
            _clientService = clientService;
            _productService = productService;
            _mapper = mapper;
        }

        #region Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerGet()
        {
            var products = _productService.GetAll();
            IList<ProductVM> productVMs = _mapper.Map<List<ProductVM>>(products);
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
            var product = _mapper.Map<Product>(productVM);
            _productService.Add(product);
            var products = _productService.GetAll();
            IList<ProductVM> productVMs = _mapper.Map<List<ProductVM>>(products);
            return View("Index", productVMs);
        }

        [HttpPost]
        public ActionResult Edit(ProductVM productVM)
        {
            var product = _mapper.Map<Product>(productVM);
            _productService.Update(product);

            return Json(new
            {
                success = true,
                Message = "OK"
            });
        }
        #endregion

        #region ClientProductValue
        public ActionResult ClientProductValue(string clienteID)
        {
            var client = _clientService.GetById(new Guid(clienteID));
            ClientVM clientsVM = _mapper.Map<ClientVM>(client);
            return View(clientsVM);
        }

        public ActionResult AjaxHandlerValorPorProduto(string clienteID)
        {
            try
            {
                var productsClients = _productService.GetClientsProductsByClientID(new Guid(clienteID));
                IList<ClientProductValueVM> productsClientsVMs = _mapper.Map<List<ClientProductValueVM>>(productsClients);

                var products = _productService.GetAll();
                IList<ProductVM> productVMs = _mapper.Map<List<ProductVM>>(products);

                IList<ClientProductValueVM> valoresProdutos = new List<ClientProductValueVM>();
                foreach (var product in productVMs)
                {
                    var prodValueExits = productsClientsVMs.Where(x => x.ProductID.ToString() == product.DT_RowId).FirstOrDefault();

                    valoresProdutos.Add(new ClientProductValueVM() { Product = product, ClientID = new Guid(clienteID), Price = (prodValueExits != null ? prodValueExits.Price : 0) });
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
        public ActionResult SubmitValorPorProduto(string clienteID, string[] lista)
        {
            JsonSerialize jsonS = new JsonSerialize();
            var clientsProductsValuesVM = jsonS.JsonDeserialize<ClientProductValueVM>(lista[0]);
            var clientsProductsValues = _mapper.Map<ICollection<ClientProductValue>>(clientsProductsValuesVM);

            _productService.ConnectRouteToClient(clientsProductsValues);

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

        public ActionResult AjaxHandlerProductStock()
        {
            try
            {
                var prodsStock = _productService.GetProductsStock();
                IList<StockProductVM> productsStocks = _mapper.Map<List<StockProductVM>>(prodsStock);
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
        public ActionResult SubmitProductStock(string[] tbProdutos)
        {
            JsonSerialize jsonS = new JsonSerialize();
            var stocksProductsVM = jsonS.JsonDeserialize<StockProductVM>(tbProdutos[0]);

            var stocksProd = _mapper.Map<ICollection<StockProduct>>(stocksProductsVM);
            var result = _productService.AddProductStock(stocksProd);
            return Json(new
            {
                success = true,
                Message = "OK"
            });
        }
        #endregion
    }
}