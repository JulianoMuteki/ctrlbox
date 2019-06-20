using System;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class RouteController : Controller
    {
        private readonly IClientApplicationService _clientApplicationService;
        private readonly IRouteApplicationService _routeApplicationService;

        public RouteController(IClientApplicationService clientService, IRouteApplicationService routeService)
        {
            _clientApplicationService = clientService;
            _routeApplicationService = routeService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerGet()
        {
            var routesVMs = _routeApplicationService.GetAll();
            
            return Json(new
            {
                aaData = routesVMs,
                success = true
            });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RouteVM routeVM)
        {
            _routeApplicationService.Add(routeVM);
            var routesVMs = _routeApplicationService.GetAll();
           
            return View("Index", routesVMs);
        }

        [HttpPost]
        public ActionResult Edit(RouteVM routeVM)
        {
            _routeApplicationService.Update(routeVM);

            return Json(new
            {
                success = true,
                Message = "OK"
            });
        }


        public ActionResult ClientRelation(string linhaID)
        {
            //caso esta linha tenha alguma entrega pendente, não pode remover o cliente
            ViewData["routeID"] = linhaID;

            var routeVM = _routeApplicationService.GetById(new Guid(linhaID));
            
            return View(routeVM);
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerClientsNotAvailable(string routeID)
        {
            try
            {
                var clientsVM = _clientApplicationService.GetNotAvailable(new Guid(routeID));

                return Json(new
                {
                    aaData = clientsVM,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerClientsAvailable(string routeID)
        {
            try
            {
                var clientsVM = _clientApplicationService.GetAvailable(new Guid(routeID));
               
                return Json(new
                {
                    aaData = clientsVM,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult PutAjaxHandlerAddClients(string[] clientsIDs)
        {
            try
            {
                JsonSerialize jsonS = new JsonSerialize();
                var routeClientVM = jsonS.JsonDeserialize<RouteClientVM>(clientsIDs[0]);

                _routeApplicationService.ConnectRouteToClient(routeClientVM);

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

        [HttpPost]
        public ActionResult PutAjaxHandlerRemoveClients(string[] clientsIDs)
        {
            try
            {
                JsonSerialize jsonS = new JsonSerialize();
                var routeClientVM = jsonS.JsonDeserialize<RouteClientVM>(clientsIDs[0]);

                _routeApplicationService.RemoveRouteFromClient(routeClientVM);

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