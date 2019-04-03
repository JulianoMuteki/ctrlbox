using System;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IClientApplicationService _clientService;
        private readonly IMapper _mapper;
        private readonly IDeliveryApplicationService _deliveryService;
        private readonly IRouteApplicationService _routeService;

        public DeliveryController(IClientApplicationService clientService, IRouteApplicationService routeService,
                                   IDeliveryApplicationService deliveryService, IMapper mapper)
        {
            _clientService = clientService;
            _routeService = routeService;
            _deliveryService = deliveryService;
            _mapper = mapper;
        }
        // GET: Delivery
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerEntregas()
        {
            try
            {
                var lista = _deliveryService.GetAll();
                return Json(new
                {
                    aaData = lista,
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
    }
}