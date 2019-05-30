using System;
using System.Collections.Generic;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class RouteController : Controller
    {
        private readonly IClientApplicationService _clientApplicationService;
        private readonly IRouteApplicationService _routeApplicationService;
        private readonly IMapper _mapper;

        public RouteController(IClientApplicationService clientService, IRouteApplicationService routeService, IMapper mapper)
        {
            _clientApplicationService = clientService;
            _routeApplicationService = routeService;
            _mapper = mapper;
        }



        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerGet()
        {
            var routes = _routeApplicationService.GetAll();
            IList<RouteVM> routesVM = _mapper.Map<List<RouteVM>>(routes);
            return Json(new
            {
                aaData = routesVM,
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
            var route = _mapper.Map<Route>(routeVM);
            _routeApplicationService.Add(route);

            var routes = _routeApplicationService.GetAll();
            IList<RouteVM> routesVM = _mapper.Map<List<RouteVM>>(routes);
            return View("Index", routesVM);
        }

        [HttpPost]
        public ActionResult Edit(RouteVM routeVM)
        {
            var route = _mapper.Map<Route>(routeVM);
            _routeApplicationService.Update(route);

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

            var route = _routeApplicationService.GetById(new Guid(linhaID));
            var routeVM = _mapper.Map<RouteVM>(route);

            return View(routeVM);
        }


        public ActionResult AjaxHandlerClientesNaoDisponiveis(string routeID)
        {
            try
            {
                var clients = _clientApplicationService.GetNotAvailable(new Guid(routeID));
                IList<ClientVM> clientsVM = _mapper.Map<List<ClientVM>>(clients);

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

        public ActionResult AjaxHandlerClientesDisponiveis(string routeID)
        {
            try
            {
                var clients = _clientApplicationService.GetAvailable(new Guid(routeID));
                IList<ClientVM> clientsVM = _mapper.Map<List<ClientVM>>(clients);

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
        public ActionResult SubmitAdicionarClientes(string linhaID, string[] clientesIDs)
        {
            try
            {
                JsonSerialize jsonS = new JsonSerialize();
                var routeClientVM = jsonS.JsonDeserialize<RouteClientVM>(clientesIDs[0]);

                var routeClient = _mapper.Map<List<RouteClient>>(routeClientVM);
                _routeApplicationService.ConnectRouteToClient(routeClient);

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
        public ActionResult SubmitRemoverClientes(string linhaID, string clientesIDs)
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