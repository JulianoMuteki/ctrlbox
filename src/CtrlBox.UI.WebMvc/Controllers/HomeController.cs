﻿using CtrlBox.UI.WebMvc.EndPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtrlBox.UI.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public ActionResult Index()
        {
            return View();
        }
    }
}