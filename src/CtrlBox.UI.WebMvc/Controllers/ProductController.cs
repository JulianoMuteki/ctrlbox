using CtrlBox.UI.WebMvc.EndPoints;
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
        private readonly ProdutoVMApi _api = null;

        public ProductController()
        {
            _api = new ProdutoVMApi("http://localhost:53929/");
        }
        // GET: Product
        public ActionResult Index()
        {
            var products = _api.GetProdutoVM("api/Product");
            return View(products);
        }


        public ActionResult ValorPorProduto(string clienteID)
        {
            ViewData["clienteID"] = clienteID;
            return View();
        }

        public ActionResult AjaxHandlerValorPorProduto(string clienteID)
        {
            try
            {
                IList<ValorPorProdutoVM> valoresProdutos = new List<ValorPorProdutoVM>();

                var products = _api.GetProdutoVMAsync("api/Product");


                //foreach (var item in products.Result)
                //{
                //    var valor = contexto.PrecoProdutos_Clientes.Where(x => x.ClienteID == new Guid(clienteID)
                //                                                                                && x.ProdutoID == item.ProdutoID).FirstOrDefault();

                //    valor = valor ?? new PrecoProdutos_Clientes();

                //    valoresProdutos.Add(new ValorPorProdutoVM(valor, item));
                //}


                return Json(new
                {
                    aaData = valoresProdutos,

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
            //var valores = lista.Split('&').ToList();

            //    foreach (var item in valores)
            //    {
            //        var val = item.Split('=');

            //        if (!string.IsNullOrEmpty(val[1]))
            //        {
            //            string id = val[0];

            //            Guid idcliente = new Guid(clienteID);
            //            Guid idProduto = new Guid(id);

            //            var valorProduto = context.PrecoProdutos_Clientes.Where(x => x.ClienteID == idcliente
            //                                                                    && x.ProdutoID == idProduto).FirstOrDefault();

            //            if (valorProduto == null)
            //            {
            //                valorProduto = new PrecoProdutos_ClientesVM()
            //                {
            //                    ClienteID = idcliente,
            //                    ProdutoID = idProduto,
            //                    Preco = Convert.ToDouble(val[1])
            //                };

            //                context.PrecoProdutos_Clientes.Add(valorProduto);
            //            }
            //            else
            //            {
            //                valorProduto.Preco = Convert.ToDouble(val[1]);
            //                context.Entry(valorProduto).State = EntityState.Modified;

            //            }

            //        }
            //    }
            //    context.SaveChanges();
            


            //var linha = context.Linhas.Where(x => x.LinhaID == idLinha).FirstOrDefault();

            //foreach (var item in clientes)
            //{
            //    var id = item.Split('=')[1];
            //    Guid idCliente = new Guid(id);

            //    var cliente = context.Clientes.Where(x => x.ClienteID == idCliente).FirstOrDefault();
            //    linha.Clientes.Remove(cliente);

            //}

            //context.SaveChanges();
            return Json(new
            {
                success = true,
                Message = "OK"
            },
                  JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProdutoVM produto)
        {
            var uri = _api.CreateProduct(produto);

            var products = _api.GetProdutoVM("api/Product");
            return View("Index", products);
        }


    }
}