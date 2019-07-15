using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class BoxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}