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
        private readonly WebApiClient _apiClient = null;
        private readonly WebApiRoute _apiRoute = null;


        public RouteController()
        {
            _apiRoute = new WebApiRoute("http://localhost:53929", "Route");
            _apiClient = new WebApiClient("http://localhost:53929", "Client");
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerGet()
        {
            var clients = _apiRoute.GetT();
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
            var uri = _apiRoute.Create(routeVM);

            var routes = _apiRoute.GetT();
            return View("Index", routes);
        }

        [HttpPost]
        public ActionResult Edit(RouteVM routeVM)
        {
            var uri = _apiRoute.Update(routeVM);

            return Json(new
            {
                success = true,
                Message = "OK"
            },
                  JsonRequestBehavior.AllowGet);
        }


        public ActionResult ClientRelation(string linhaID)
        {
            //caso esta linha tenha alguma entrega pendente, não pode remover o cliente
            ViewBag.linhaID = linhaID;

            var routeVM = _apiRoute.GetT(new Guid(linhaID));
            return View(routeVM);
        }


        public ActionResult AjaxHandlerClientesNaoDisponiveis(string linhaID)
        {
            try
            {
                var clientesVM = _apiClient.GetNotAvailable(new Guid(linhaID));

                return Json(new
                {
                    aaData = clientesVM,
                    success = true
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AjaxHandlerClientesDisponiveis(string linhaID)
        {
            try
            {
                var clientesVM = _apiClient.GetAvailable(new Guid(linhaID));

                return Json(new
                {
                    aaData = clientesVM,
                    success = true
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SubmitAdicionarClientes(string linhaID, string clientesIDs)
        {
            try
            {
                var clientesVM = _apiRoute.ConnectRouteToClient(new RouteVM(linhaID, clientesIDs));

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
            },
                  JsonRequestBehavior.AllowGet);

        }
    }
}