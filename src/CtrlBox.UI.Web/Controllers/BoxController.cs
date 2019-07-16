using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class BoxController : Controller
    {
        private readonly IMapper _mapper;

        public BoxController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BoxesType()
        {
            return View();
        }

        public IActionResult GetAjaxHandlerBoxesType()
        {
            return View();
        }
    }
}