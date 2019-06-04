using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
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

                return Json(new
                {
                    aaData = clientsProductsVM.Select(x => new
                    {
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
        public ActionResult SubmitData(string[] strSaleJSON)
        {
            try
            {
                JsonSerialize jsonS = new JsonSerialize();
                var vendas_Produtos = jsonS.JsonDeserializeObject<SaleVM>(strSaleJSON[0]);

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