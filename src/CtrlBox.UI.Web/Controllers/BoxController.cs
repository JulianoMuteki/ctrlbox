using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace CtrlBox.UI.Web.Controllers
{
    public class BoxController : Controller
    {
        private readonly IBoxApplicationService _boxApplicationService;
        private readonly IProductApplicationService _productApplicationService;

        public BoxController(IBoxApplicationService boxApplicationService, IProductApplicationService productApplicationService)
        {
            _boxApplicationService = boxApplicationService;
            _productApplicationService = productApplicationService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var boxesType = _boxApplicationService.GetAllBoxesType()
                                                    .Select(boxType => new SelectListItem
                                                    {
                                                        Value = boxType.DT_RowId,
                                                        Text = boxType.Name
                                                    }).ToList();
            ViewData["BoxesType"] = boxesType;

            var products = _productApplicationService.GetAll()
                                        .Select(prod => new SelectListItem
                                        {
                                            Value = prod.DT_RowId,
                                            Text = $"{prod.Name} {prod.UnitMeasure}"
                                        }).ToList();
            ViewData["Products"] = products;
            return View();
        }

        [HttpPost]
        public IActionResult Create(BoxVM boxTypeVM)
        {
            try
            {


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