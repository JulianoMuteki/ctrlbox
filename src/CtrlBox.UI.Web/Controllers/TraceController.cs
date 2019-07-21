using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class TraceController : Controller
    {
        private readonly ITraceabilityApplicationService _traceabilityApplicationService;

        public TraceController(ITraceabilityApplicationService traceabilityApplicationService)
        {
            _traceabilityApplicationService = traceabilityApplicationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TracesTypes()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAjaxHandlerTracesTypes()
        {
            try
            {
                var tracesTypes = _traceabilityApplicationService.GetAllTracesTypes();

                return Json(new
                {
                    aaData = tracesTypes,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult CreateTraceType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTraceType(TraceTypeVM traceTypeVM)
        {
            try
            {
                _traceabilityApplicationService.AddTraceType(traceTypeVM);
                return RedirectToAction("Index");
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