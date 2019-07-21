using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CtrlBox.UI.Web.Controllers
{
    public class TraceController : Controller
    {
        private readonly ITraceabilityApplicationService _traceabilityApplicationService;
        private readonly IBoxApplicationService _boxApplicationService;
        private readonly IProductApplicationService _productApplicationService;

        public TraceController(ITraceabilityApplicationService traceabilityApplicationService, IBoxApplicationService boxApplicationService, IProductApplicationService productApplicationService)
        {
            _boxApplicationService = boxApplicationService;
            _productApplicationService = productApplicationService;
            _traceabilityApplicationService = traceabilityApplicationService;
        }

        public IActionResult Index(Guid boxID)
        {
            var boxVM = _boxApplicationService.GetBoxesByIDWithBoxTypeAndProductItems(boxID);
            ViewData["BoxViewData"] = boxVM;//$"{boxVM.Barcode} - {boxVM.Description} - {boxVM.BoxType.Name}";

            var traces = _traceabilityApplicationService.GetByBoxID(boxID);
            return View(traces);
        }

        public IActionResult Create()
        {
            var boxes = _boxApplicationService.GetAll()
                                           .Select(box => new SelectListItem
                                           {
                                               Value = box.DT_RowId,
                                               Text = $"{box.Barcode} - {box.Description }"
                                           }).ToList();

            var productsItems = _productApplicationService.GetProductsItems()
                                        .Select(prod => new SelectListItem
                                        {
                                            Value = prod.DT_RowId,
                                            Text = $"{prod.Barcode} - {prod.Product.Name }"
                                        }).ToList();

            var tracesTypes = _traceabilityApplicationService.GetAllTracesTypes()
                            .Select(trace => new SelectListItem
                            {
                                Value = trace.DT_RowId,
                                Text = trace.Description
                            }).ToList();

            ViewData["Boxes"] = boxes;
            ViewData["ProductsItems"] = productsItems;
            ViewData["TracesTypes"] = tracesTypes;

            return View();
        }

        [HttpPost]
        public IActionResult Create(TraceabilityVM traceabilityVM)
        {
            try
            {
                _traceabilityApplicationService.Add(traceabilityVM);
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
                return RedirectToAction("TracesTypes");
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