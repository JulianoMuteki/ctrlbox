using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CtrlBox.UI.Web.Controllers
{
    public class SaleController : Controller
    {
        private readonly IClientApplicationService _clientService;
        private readonly IDeliveryApplicationService _deliveryService;
        private readonly IRouteApplicationService _routeService;
        private readonly IProductApplicationService _productService;
        private readonly ISaleApplicationService _saleService;
        private readonly IPaymentApplicationService _paymentService;

        public SaleController(IClientApplicationService clientService, IRouteApplicationService routeService,
                                   IDeliveryApplicationService deliveryService, IProductApplicationService productService,
                                   ISaleApplicationService saleService, IPaymentApplicationService paymentService)
        {
            _clientService = clientService;
            _routeService = routeService;
            _deliveryService = deliveryService;
            _productService = productService;
            _saleService = saleService;
            _paymentService = paymentService;
        }
        // GET: Sale
        public ActionResult Index(string linhaID, string clienteID, string entregaID)
        {
            try
            {
                ViewData["clienteID"] = clienteID;
                ViewData["linhaID"] = linhaID;
                ViewData["entregaID"] = entregaID;

                var saleVM = _saleService.GetByClientIDAndDeliveryID(new Guid(clienteID), new Guid(entregaID));
                saleVM = saleVM ?? new SaleVM();
                ViewData["saleID"] = saleVM.DT_RowId;
                return View(saleVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerExecuteSale(string clienteID, string linhaID, string deliveryID)
        {
            try
            {
                Guid clientID = new Guid(clienteID);

                //Busca preço de produtos por clientes. Deve sempre existir preço para todos clientes
                var clientsProductsVM = _productService.GetClientsProductsByClientID(clientID);
                var productsDeliveryVM = _productService.GetDeliveryProducts(new Guid(deliveryID));

                if (clientsProductsVM.Count == 0)
                    throw CustomException.Create<SaleController>("There aren't products for this client.", nameof(this.GetAjaxHandlerExecuteSale));

                return Json(new
                {
                    aaData = productsDeliveryVM.Select(x => new
                    {
                        DT_RowId = x.ProductID.ToString(),
                        NomeProduto = x.Product.Name,
                        ValorProduto = String.Format("{0:c}", (from c in clientsProductsVM where c.ProductID == x.ProductID select c.Price).FirstOrDefault()),
                        x.Amount,
                        x.Product.UnitMeasure,
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
        public ActionResult PostAjaxHandlerAddSale(string[] strSaleJSON)
        {
            try
            {
                JsonSerialize jsonS = new JsonSerialize();
                var saleVM = jsonS.JsonDeserializeObject<SaleVM>(strSaleJSON[0]);
                _saleService.Add(saleVM);

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

        public ActionResult Invoice(Guid saleID)
        {
            var saleVM = _saleService.GetInvoiceSaleByID(saleID);
            return View(saleVM);
        }

        public ActionResult GetAjaxHandlerPayMethods()
        {
            var usersList = _paymentService.GetPayMethods()
                                       .Select(method => new SelectListItem
                                       {
                                           Value = method.DT_RowId,
                                           Text = method.MethodName
                                       }).ToList();

            return Json(new
            {
                aaData = usersList,
                success = true
            });
        }
    }
}