using AutoMapper;
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

            var categories = _clientApplicationService.GetAllCategories()
                            .Select(category => new SelectListItem
                            {
                                Value = category.DT_RowId,
                                Text = category.Name
                            }).ToList();
            ViewData["Categories"] = categories;

            return View(clientVM);
        }

        [HttpPost]
        public ActionResult Create(ClientVM clientVM)
        {
            if (string.IsNullOrEmpty(clientVM.DT_RowId))
                _clientApplicationService.Add(clientVM);
            else
                _clientApplicationService.Update(clientVM);

            return RedirectToAction("Index");
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

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryVM categoryVM)
        {
            _clientApplicationService.AddCategory(categoryVM);
            return View();
        }

        public ActionResult IndexCategories()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAjaxHandlerCategories()
        {
            var categories = _clientApplicationService.GetAllCategories();
            return Json(new
            {
                aaData = categories
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