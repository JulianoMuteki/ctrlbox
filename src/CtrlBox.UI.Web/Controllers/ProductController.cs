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
                IList<ClientProductValueVM> valoresProdutos = new List<ClientProductValueVM>();
                var products = _productService.GetAll();
                IList<ProductVM> productVMs = _mapper.Map<List<ProductVM>>(products);

                foreach (var product in productVMs)
                {
                    valoresProdutos.Add(new ClientProductValueVM() { Product = product });
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
        public ActionResult SubmitValorPorProduto(string clienteID, string lista)
        {
            var valores = lista.Split('&').ToList();

            ICollection<ClientProductValueVM> productsClients = new List<ClientProductValueVM>();

            foreach (var item in valores)
            {
                var val = item.Split('=');

                if (!string.IsNullOrEmpty(val[1]))
                {
                    string id = val[0];

                    Guid idcliente = new Guid(clienteID);
                    Guid idProduto = new Guid(id);

                    ClientProductValueVM x = new ClientProductValueVM()
                    {
                        ClientID = idcliente,
                        ProductID = idProduto,
                        Price = float.Parse(val[1])
                    };

                    productsClients.Add(x);
                }
            }

            var clientsProducts = _mapper.Map<ICollection<ClientProductValue>>(productsClients);

             _productService.ConnectRouteToClient(clientsProducts);
           
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
                ICollection<StockProductVM> productsStockVM = _mapper.Map<List<StockProductVM>>(prodsStock);
                return Json(new
                {
                    aaData = productsStockVM
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