using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace CtrlBox.UI.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientApplicationService _clientApplicationService;
        private readonly IAddressApplicationService _addressApplicationService;

        public ClientController(IClientApplicationService clientService, IAddressApplicationService addressApplicationService)
        {
            _clientApplicationService = clientService;
            _addressApplicationService = addressApplicationService;
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

        public ActionResult Create(Guid clientID)
        {
            var clientVM = _clientApplicationService.GetById(clientID);

            if (clientVM != null)
                clientVM.SetClientsCategoriesID();

            var addresses = _addressApplicationService.GetAll()
                                        .Select(address => new SelectListItem
                                        {
                                            Value = address.DT_RowId,
                                            Text = $"{address.Street} - {address.Number} - {address.City} - {address.CEP}"
                                        }).ToList();
            ViewData["Addresses"] = addresses;

            var optionsTypes = _clientApplicationService.GetAllOptionsTypes()
                            .Select(option => new SelectListItem
                            {
                                Value = option.DT_RowId,
                                Text = $"{option.Name} - {option.EClientType}"
                            }).ToList();
            ViewData["OptionsTypes"] = optionsTypes;

            return View(clientVM);
        }

        [HttpPost]
        public ActionResult Create(ClientVM clientVM)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(clientVM.DT_RowId))
                    clientVM = _clientApplicationService.Add(clientVM);
                else
                    clientVM = _clientApplicationService.Update(clientVM);

                if (clientVM.HasNotifications)
                    return View(clientVM);

                return RedirectToAction("Index");
            }
            else
                return View(clientVM);
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

        public ActionResult OptionsTypes()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerOptionsTypes()
        {
            var optionsTypes = _clientApplicationService.GetAllOptionsTypes();
            return Json(new
            {
                aaData = optionsTypes
            });
        }

        public ActionResult CreateOptionType()
        {
            var clientsTypes = Enum.GetNames(typeof(EClientType))
                                        .Select(name => new SelectListItem
                                        {
                                            Value = name,
                                            Text = name
                                        }).ToList();
            ViewData["ClientsTypes"] = clientsTypes;

            return View();
        }

        [HttpPost]
        public ActionResult CreateOptionType(OptiontTypeVM optiontTypeVM)
        {
            _clientApplicationService.AddOptionType(optiontTypeVM);
            return RedirectToAction("OptionsTypes");
        }

    }
}