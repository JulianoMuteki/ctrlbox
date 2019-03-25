using CtrlBox.UI.WebMvc.EndPoints;
using CtrlBox.UI.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtrlBox.UI.WebMvc.Controllers
{
    public class RouteController : Controller
    {
        private readonly WebApiClient<RouteVM> _api = null;

        public RouteController()
        {
            _api = new WebApiClient<RouteVM>("http://localhost:53929", "Route");
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
        public ActionResult Create(RouteVM routeVM)
        {
            var uri = _api.Create(routeVM);

            var routes = _api.GetT();
            return View("Index", routes);
        }

        [HttpPost]
        public ActionResult Edit(RouteVM routeVM)
        {
            var uri = _api.Update(routeVM);

            return Json(new
            {
                success = true,
                Message = "OK"
            },
                  JsonRequestBehavior.AllowGet);
        }
    }
}