using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CtrlBox.UI.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientApplicationService _clientApplicationService;
        private readonly IMapper _mapper;

        public ClientController(IClientApplicationService clientService, IMapper mapper)
        {
            _clientApplicationService = clientService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerGet()
        {
            var clients = _clientApplicationService.GetAll();
            IList<ClientVM> clientsVM = _mapper.Map<List<ClientVM>>(clients);
            return Json(new
            {
                aaData = clientsVM
            });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClientVM clientVM)
        {
            var client = _mapper.Map<Client>(clientVM);
            _clientApplicationService.Add(client);
            var clients = _clientApplicationService.GetAll();
            return View("Index", clients);
        }

        [HttpPost]
        public ActionResult Edit(ClientVM clientVM)
        {
            var client = _mapper.Map<Client>(clientVM);
            _clientApplicationService.Update(client);

            return Json(new
            {
                success = true,
                Message = "OK"
            });
        }
    }
}