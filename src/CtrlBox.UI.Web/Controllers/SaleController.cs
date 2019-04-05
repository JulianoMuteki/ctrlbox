using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class SaleController : Controller
    {
        private readonly IClientApplicationService _clientService;
        private readonly IMapper _mapper;
        private readonly IDeliveryApplicationService _deliveryService;
        private readonly IRouteApplicationService _routeService;
        private readonly IProductApplicationService _productService;

        public SaleController(IClientApplicationService clientService, IRouteApplicationService routeService,
                                   IDeliveryApplicationService deliveryService, IProductApplicationService productService, IMapper mapper)
        {
            _clientService = clientService;
            _routeService = routeService;
            _deliveryService = deliveryService;
            _productService = productService;
            _mapper = mapper;
        }
        // GET: Sale
        public ActionResult Index(string linhaID, string clienteID, string entregaID)
        {
            ViewData["clienteID"] = clienteID;
            ViewData["linhaID"] = linhaID;
            ViewData["entregaID"] = entregaID;
            return View();
        }

        public ActionResult AjaxHandlerExecutarVenda(string clienteID, string linhaID)
        {
            try
            {
                Guid clientID = new Guid(clienteID);

                //Busca preço de produtos por clientes. Deve sempre existir preço para todos clientes
                var clientsProducts = _productService.GetClientsProductsByClientID(clientID);
                IList<ClientProductValueVM> clientsProductsVM = _mapper.Map<List<ClientProductValueVM>>(clientsProducts);


                var products = _productService.GetAll();
                IList<ProductVM> productVMs = _mapper.Map<List<ProductVM>>(products);


                //foreach (var item in produtos)
                //{
                //    cadastroVenda.ProdutosPrecos.Add(new ProdutoPrecoVM()
                //    {
                //        DT_RowId = item.ProdutoID.ToString(),
                //        NomeProduto = item.Nome,
                //        ValorProduto = String.Format("{0:c}", (from x in precoProdutos where x.ProdutoID == item.ProdutoID && x.ClienteID == clientID select x).FirstOrDefault().Preco),
                //        QtdeVenda = 0,
                //        QtdeRetorno = 0,
                //        Total = String.Format("{0:c}", 0)
                //    });

                //}

                return Json(new
                {
                    aaData = clientsProductsVM.Select(x => new {
                                                            DT_RowId = x.ProductID.ToString(),
                                                            NomeProduto = (from p in productVMs where p.DT_RowId == x.ProductID.ToString() select p.Name),
                                                            ValorProduto = String.Format("{0:c}", x.Price),
                                                            QtdeVenda = 0,
                                                            QtdeRetorno = 0,
                                                            Total = String.Format("{0:c}", 0)
                                                        }),
                    success = true,
                    SaldoAnterior = 0,
                    CaixasEmDebito = 0
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SubmitData(string entregaID, string clienteID, string[] tbVenda, string ValorRecebido, string valorPrazo, string retornoCaixa, string[] tbCheques)
        {
            try
            {
                //ModelCodeFirst context = new ModelCodeFirst();
                //Guid entID = new Guid(entregaID); // new Guid(ViewData["entregaID"].ToString());

                //JsonSerialize jsonS = new JsonSerialize();
                //var vendas_Produtos = jsonS.JsonDeserialize<ProdutoPrecoVM>(tbVenda[0]);

                //if (tbCheques[0].Count() > 0)
                //{
                //    var cheques = jsonS.JsonDeserialize<ChequeVM>(tbCheques[0]);
                //}


                //Venda venda = new Venda()
                //{
                //    VendaID = Guid.NewGuid(),
                //    ClienteID = new Guid(clienteID),
                //    EntregaID = entID,
                //    ValorRecebido = Convert.ToDecimal(ValorRecebido),
                //    ValorAPrazo = Convert.ToDecimal(valorPrazo),
                //    CaixasRetornadas = Convert.ToInt32(retornoCaixa)

                //};
                //context.Vendas.Add(venda);

                //foreach (var item in vendas_Produtos)
                //{
                //    Vendas_Produtos vendaProduto = new Vendas_Produtos()
                //    {
                //        VendasID = venda.VendaID,
                //        ProdutoID = new Guid(item.DT_RowId),
                //        Quantidade = Convert.ToInt32(item.QtdeVenda),
                //        QuantidadeTroca = Convert.ToInt32(item.QtdeRetorno),
                //        ValorVenda = Convert.ToDecimal(item.ValorProduto)
                //    };

                //    context.Vendas_Produtos.Add(vendaProduto);

                //    var entregaProduto = context.Entregas_Produtos.Where(x => x.ProdutoID == vendaProduto.ProdutoID && x.EntregaID == entID).FirstOrDefault();
                //    entregaProduto.Quantidade -= (int)vendaProduto.Quantidade;

                //    context.Entregas_Produtos.Attach(entregaProduto);
                //    context.Entry(entregaProduto).State = EntityState.Modified;
                //}



              
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

    }
}