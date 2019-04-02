using CtrlBox.Application.ViewModel;
using CtrlBox.UI.WebMvc.EndPoints;
using System.Web.Mvc;

namespace CtrlBox.UI.WebMvc.Controllers
{
    public class ClientController : Controller
    {
        private readonly WebApiClient _api = null;

        public ClientController()
        {
            _api = new WebApiClient("http://localhost:53929", "Client");
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerGet()
        {
            var clients = _api.GetT();
            return Json(new
            {
                aaData = clients,
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClientVM produto)
        {
            var uri = _api.Create(produto);

            var products = _api.GetT();
            return View("Index", products);
        }

        [HttpPost]
        public ActionResult Edit(ClientVM produtoVM)
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
    }
}