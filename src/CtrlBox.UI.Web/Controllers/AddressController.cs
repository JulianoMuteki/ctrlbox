using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressApplicationService _addressApplicationService;
        private readonly IMapper _mapper;

        public AddressController(IAddressApplicationService clientService, IMapper mapper)
        {
            _addressApplicationService = clientService;
            _mapper = mapper;
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