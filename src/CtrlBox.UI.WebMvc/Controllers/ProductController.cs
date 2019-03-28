using CtrlBox.UI.WebMvc.EndPoints;
using CtrlBox.UI.WebMvc.Helpers;
using CtrlBox.UI.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtrlBox.UI.WebMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly WebApiProduct _api = null;

        private readonly WebApiClient _clientApi = null;

        public ProductController()
        {
            _api = new WebApiProduct("http://localhost:53929", "Product");
            _clientApi = new WebApiClient("http://localhost:53929", "Client");

        }

        #region Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerGet()
        {
            var products = _api.GetT();
            return Json(new
            {
                aaData = products,
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProdutoVM produto)
        {
            var uri = _api.Create(produto);

            var products = _api.GetT();
            return View("Index", products);
        }

        [HttpPost]
        public ActionResult Edit(ProdutoVM produtoVM)
        {
            var uri = _api.Update(produtoVM);

            var products = _api.GetT();
            return Json(new
            {
                success = true,
                Message = "OK"
            },
                  JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ClientProductValue
        public ActionResult ClientProductValue(string clienteID)
        {
            var clientVM = _clientApi.GetT(new Guid(clienteID));
            return View(clientVM);
        }

        public ActionResult AjaxHandlerValorPorProduto(string clienteID)
        {
            try
            {
                IList<ValorPorProdutoVM> valoresProdutos = new List<ValorPorProdutoVM>();
                var products = _api.GetT();

                foreach (var product in products)
                {
                    valoresProdutos.Add(new ValorPorProdutoVM(new ClientProductValueVM(), product));
                }
                return Json(new
                {
                    aaData = valoresProdutos

                    // SaldoAnterior = cadastroVenda.SaldoAnterior,
                    // CaixasEmDebito = cadastroVenda.CaixasEmDebito
                },
                JsonRequestBehavior.AllowGet);
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

            var products = _api.ConnectProductToClient(productsClients);
            return Json(new
            {
                success = true,
                Message = "OK"
            },
                   JsonRequestBehavior.AllowGet);
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
                IList<StockProductVM> stocksProducts = new List<StockProductVM>();
                var products = _api.GetT();

                foreach (var product in products)
                {
                    stocksProducts.Add(new StockProductVM() { ProductID = product.DT_RowId, Name = product.Name });
                }
                return Json(new
                {
                    aaData = stocksProducts
                },
                JsonRequestBehavior.AllowGet);
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

            var stocksProducts = _api.AddProductStock(stocksProductsVM);
            return Json(new
            {
                success = true,
                Message = "OK"
            },
                   JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}