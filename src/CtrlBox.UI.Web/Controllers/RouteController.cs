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


        public ActionResult AjaxHandlerCustomersNotAvailable(string routeID)
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

        public ActionResult AjaxHandlerCustomersAvailable(string routeID)
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
        public ActionResult SubmitAddClients(string linhaID, string[] clientesIDs)
        {
            try
            {
                JsonSerialize jsonS = new JsonSerialize();
                var routeClientVM = jsonS.JsonDeserialize<RouteClientVM>(clientesIDs[0]);

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
        public ActionResult SubmitRemoveClients(string linhaID, string clientesIDs)
        {
            //var clientes = clientesIDs.Split('&').ToList();
            //ModelCodeFirst context = new ModelCodeFirst();
            //Guid idLinha = new Guid(linhaID);

            //var linha = context.Linhas.Where(x => x.LinhaID == idLinha).FirstOrDefault();

            //foreach (var item in clientes)
            //{
            //    var id = item.Split('=')[1];
            //    Guid idCliente = new Guid(id);

            //    var cliente = context.Clientes.Where(x => x.ClienteID == idCliente).FirstOrDefault();
            //    linha.Clientes.Remove(cliente);

            //}

            //context.SaveChanges();
            return Json(new
            {
                success = true,
                Message = "OK"
            });

        }
    }
}