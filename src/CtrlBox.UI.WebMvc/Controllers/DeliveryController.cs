using CtrlBox.Application.ViewModel;
using CtrlBox.UI.WebMvc.EndPoints;
using CtrlBox.UI.WebMvc.Helpers;
using System;
using System.Web.Mvc;

namespace CtrlBox.UI.WebMvc.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly WebApiClient _apiClient = null;
        private readonly WebApiRoute _apiRoute = null;
        public readonly WebApiDelivery _apiDelivery = null;

        public DeliveryController()
        {
            _apiRoute = new WebApiRoute("http://localhost:53929", "Route");
            _apiClient = new WebApiClient("http://localhost:53929", "Client");
            _apiDelivery = new WebApiDelivery("http://localhost:53929", "Delivery");
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
                var lista = _apiDelivery.GetT();
                return Json(new
                {
                    aaData = lista,

                    success = true
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

                _apiDelivery.Create(deliveryVM);
                return Json(new
                {
                    success = true,
                    Message = "OK"
                },
                     JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}