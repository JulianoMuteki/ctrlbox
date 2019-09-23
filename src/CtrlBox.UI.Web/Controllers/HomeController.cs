using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CtrlBox.UI.Web.Models;
using CtrlBox.Application.ViewModel;
using Microsoft.AspNetCore.Diagnostics;

namespace CtrlBox.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View( new List<ClientVM>());
        }

        public IActionResult LayoutTable()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var excptionDetail = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //excptionDetail.Path;
            //excptionDetail.Error.Message;
            //excptionDetail.Error.Source;
            //excptionDetail.Error.StackTrace;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Page500()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Page404()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult PageTemplate()
        {
            return View();
        }

    }
}
