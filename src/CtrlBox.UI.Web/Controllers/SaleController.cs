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
        private readonly IBoxApplicationService _boxService;

        public SaleController(IClientApplicationService clientService, IRouteApplicationService routeService,
                                   IDeliveryApplicationService deliveryService, IProductApplicationService productService,
                                   ISaleApplicationService saleService, IPaymentApplicationService paymentService,
                                   IBoxApplicationService boxService)
        {
            _clientService = clientService;
            _routeService = routeService;
            _deliveryService = deliveryService;
            _productService = productService;
            _saleService = saleService;
            _paymentService = paymentService;
            _boxService = boxService;
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
                var boxesProductItemsVM = _boxService.GetBoxesBoxesProductItemsByDeliveryID(new Guid(deliveryID));

                //if (clientsProductsVM.Count == 0)
                //    throw CustomException.Create<SaleController>("There aren't products for this client.", nameof(this.GetAjaxHandlerExecuteSale));

                var boxesProductItemsGroup = boxesProductItemsVM.GroupBy(item => item.ProductItem.Product.DT_RowId,
                                                                  (key, group) => new {
                                                                      DT_RowId = key,
                                                                      NomeProduto = group.Select(x=>x.ProductItem.Product.Name).FirstOrDefault(),
                                                                      UnitMeasure = group.Select(x => x.ProductItem.Product.UnitMeasure).FirstOrDefault(),
                                                                      TotalBox = group.Select(p=>p.ProductItem).Count()
                                                                  })
                                                         .ToList();

                return Json(new
                {
                    aaData = boxesProductItemsGroup.Select(x => new
                    {
                        DT_RowId = x.DT_RowId.ToString(),
                        x.NomeProduto,
                        ValorProduto = String.Format("{0:c}", (from c in clientsProductsVM where c.ProductID.ToString() == x.DT_RowId select c.Price).FirstOrDefault()),
                        x.TotalBox,
                        x.UnitMeasure,
                        //QtdeVenda = 0,
                        //QtdeRetorno = 0,
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