using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CtrlBox.UI.Web.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressApplicationService _addressApplicationService;

        public AddressController(IAddressApplicationService clientService)
        {
            _addressApplicationService = clientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAjaxHandlerAddresses()
        {
            var adrressesVMs = _addressApplicationService.GetAll();

            return Json(new
            {
                aaData = adrressesVMs,
                success = true
            });
        }

        [HttpGet]
        public IActionResult GetAjaxHandlerAddressByID(string addressID)
        {
            var adrressesVMs = _addressApplicationService.GetById(new Guid(addressID));

            return Json(new
            {
                aaData = adrressesVMs,
                success = (adrressesVMs != null)
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Create(AddressVM addressVM)
        {
            _addressApplicationService.Add(addressVM);
            return RedirectToAction("Index");
        }
    }
}