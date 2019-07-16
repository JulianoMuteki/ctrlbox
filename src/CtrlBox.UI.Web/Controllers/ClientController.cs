using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientApplicationService _clientApplicationService;

        public ClientController(IClientApplicationService clientService)
        {
            _clientApplicationService = clientService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerGet()
        {
            var clients = _clientApplicationService.GetAll();
            return Json(new
            {
                aaData = clients
            });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClientVM clientVM)
        {
            _clientApplicationService.Add(clientVM);
            var clients = _clientApplicationService.GetAll();
            return View("Index", clients);
        }

        [HttpPost]
        public ActionResult Edit(ClientVM clientVM)
        {
            _clientApplicationService.Update(clientVM);

            return Json(new
            {
                success = true,
                Message = "OK"
            });
        }
    }
}