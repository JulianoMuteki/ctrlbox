using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressApplicationService _clientApplicationService;
        private readonly IMapper _mapper;

        public AddressController(IAddressApplicationService clientService, IMapper mapper)
        {
            _clientApplicationService = clientService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Create(AddressVM addressVM)
        {
            _clientApplicationService.Add(addressVM);
            return RedirectToAction("Index");
        }
    }
}