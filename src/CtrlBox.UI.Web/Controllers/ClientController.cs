using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CtrlBox.UI.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientApplicationService _clientApplicationService;
        private readonly IAddressApplicationService _addressApplicationService;
        private object categories;

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

        public ActionResult Create()
        {
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

            return View();
        }

        [HttpPost]
        public ActionResult Create(ClientVM clientVM)
        {
            _clientApplicationService.Add(clientVM);
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
    }
}