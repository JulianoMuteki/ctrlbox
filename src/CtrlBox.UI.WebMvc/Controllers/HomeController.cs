using CtrlBox.UI.WebMvc.EndPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtrlBox.UI.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutoVMApi _api = null;

        public HomeController()
        {
            _api = new ProdutoVMApi("http://localhost:64195/");

        }
        public ActionResult Index()
        {
           
            var product = _api.GetProdutoVMAsync("api/Product");

            return View();
        }
    }
}