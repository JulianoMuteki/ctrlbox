using System;
using System.Collections.Generic;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CtrlBox.UI.Web.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IClientApplicationService _clientService;
        private readonly IMapper _mapper;
        private readonly IDeliveryApplicationService _deliveryService;
        private readonly IRouteApplicationService _routeService;
        private readonly IProductApplicationService _productService;

        public DeliveryController(IClientApplicationService clientService, IRouteApplicationService routeService,
                                   IDeliveryApplicationService deliveryService, IProductApplicationService productService, IMapper mapper)
        {
            _clientService = clientService;
            _routeService = routeService;
            _deliveryService = deliveryService;
            _productService = productService;
            _mapper = mapper;
        }
        // GET: Delivery
        public ActionResult Index()
        {
            ViewBag.Stock = "Check stock of the product: XXXXXXXX";
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerEntregas()
        {
            try
            {
                var deliveries = _deliveryService.GetAll();
                var deliveriesVM = _mapper.Map<ICollection<DeliveryVM>>(deliveries);

                return Json(new
                {
                    aaData = deliveriesVM,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitCadastrarEntrega(string[] tbProdutos, string linha)
        {
            try
            {
                JsonSerialize jsonS = new JsonSerialize();
                var deliveryProductsVMs = jsonS.JsonDeserialize<DeliveryProductVM>(tbProdutos[0]);

                DeliveryVM deliveryVM = new DeliveryVM();
                deliveryVM.RouteID = new Guid(linha);
                deliveryVM.DeliveriesProducts = deliveryProductsVMs;

                Delivery delivery = _mapper.Map<Delivery>(deliveryVM);
                _deliveryService.Add(delivery);
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



        public ActionResult ExecuteDelivery(string entregaID, string linhaID)
        {
            ViewData["entregaID"] = entregaID;
            ViewData["linhaID"] = linhaID;
            return View();
        }

        public ActionResult AjaxHandlerExecutarEntrega(string entregaID)
        {
            try
            {
                Guid idEntrega = new Guid(entregaID);
                var delivery = _deliveryService.GetById(idEntrega);
                var deliveryVM = _mapper.Map<DeliveryVM>(delivery);

                var route = _routeService.GetById(delivery.RouteID);
                var routeVM = _mapper.Map<RouteVM>(route);

                var clients = _clientService.GetByRouteID(new Guid(routeVM.DT_RowId));
                var clientsVM = _mapper.Map<ICollection<ClientVM>>(clients);

                var productsDelivery = _productService.GetDeliveryProducts(new Guid(deliveryVM.DT_RowId));
                var productsDeliveryVM = _mapper.Map<ICollection<DeliveryProductVM>>(productsDelivery);

                ICollection<ExpenseVM> despesasVM = new List<ExpenseVM>();

                return Json(new
                {
                    aaData = clientsVM,
                    xaData = productsDeliveryVM.Select(x=> new { x.Product.Name, x.Product.DT_RowId, x.DeliveryID, x.Amount }).ToList(),
                    xbData = despesasVM,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SubmitDespesa(string entregaID, string descricao, string valor)
        {
            Guid idEntrega = new Guid(entregaID);

            ExpenseVM despesa = new ExpenseVM()
            {
                //DespesaID = Guid.NewGuid(),
                //EntregaID = idEntrega,
                //Descricao = descricao,
                //Valor = Convert.ToDouble(valor)
            };
            //context.Despesas.Add(despesa);
            //context.SaveChanges();

            return Json(new
            {
                success = true
            });
        }
    }
}