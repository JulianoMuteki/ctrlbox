﻿using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CtrlBox.UI.Web.Controllers
{
    public class BoxController : Controller
    {
        private readonly IBoxApplicationService _boxApplicationService;

        public BoxController(IBoxApplicationService boxApplicationService)
        {
            _boxApplicationService = boxApplicationService;
                
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
            try
            {
                var boxesTypVM = _boxApplicationService.GetAllBoxesType();

                return Json(new
                {
                    aaData = boxesTypVM,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult CreateBoxType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBoxType(BoxTypeVM boxTypeVM)
        {
            try
            {
                _boxApplicationService.AddBoxType(boxTypeVM);

                return RedirectToAction("BoxesType");
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}