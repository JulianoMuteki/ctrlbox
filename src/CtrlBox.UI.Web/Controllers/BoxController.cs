using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        public IActionResult GetAjaxHandlerBoxes()
        {
            try
            {
                var boxesVM = _boxApplicationService.GetAll();

                return Json(new
                {
                    aaData = boxesVM,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult GetAjaxHandlerBoxesParents()
        {
            try
            {
                var boxesVM = _boxApplicationService.BoxesParents();

                return Json(new
                {
                    aaData = boxesVM.GroupBy(n => new { n.BoxTypeID, n })
                                                .Select(g => new
                                                {
                                                    DT_RowId = g.Key.BoxTypeID,
                                                    BoxType = g.Key.n.BoxType.Name,
                                                    TotalBox = g.Count()
                                                }),


                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Create()
        {
            var boxesType = _boxApplicationService.GetAllBoxesType()
                                                    .Select(boxType => new SelectListItem
                                                    {
                                                        Value = boxType.DT_RowId,
                                                        Text = $"{boxType.Name} - {boxType.Description}"
                                                    }).ToList();
            ViewData["BoxesType"] = boxesType;

            var products = _productApplicationService.GetAll()
                                        .Select(prod => new SelectListItem
                                        {
                                            Value = prod.DT_RowId,
                                            Text = $"{prod.Name} - {prod.Description} - {prod.Package} - {prod.Capacity}{prod.UnitMeasure}"
                                        }).ToList();
            ViewData["Products"] = products;

            var boxes = _boxApplicationService.GetBoxesParentsWithBoxTypeEndProduct()
                            .Select(box => new SelectListItem
                            {
                                Value = box.DT_RowId,
                                Text = $"{box.BoxBarcode.BarcodeEAN13} {box.Description}"
                            }).ToList();

            ViewData["Boxes"] = boxes;

            return View();
        }

        [HttpPost]
        public IActionResult Create(BoxVM boxVM)
        {
            try
            {
                _boxApplicationService.Add(boxVM);
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

        [HttpGet]
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
            var optionsBoxLengthUnit = CtrlBoxUnits.CtrlBoxLengthUnit
                   .Select(unit => new SelectListItem
                   {
                       Value = unit,
                       Text = unit
                   }).ToList();
            ViewData["OptionsBoxLengthUnit"] = optionsBoxLengthUnit;
            return View();
        }

        [HttpPost]
        public IActionResult CreateBoxType(BoxTypeVM boxTypeVM, IFormFile FilePicture)
        {
            try
            {
                PictureVM imageEntity = GeneratePicture.CreatePicture(FilePicture, $"{boxTypeVM.Name} - {boxTypeVM.Description}");
                boxTypeVM.Picture = imageEntity;

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

        public IActionResult ViewBoxes(Guid boxFatherID)
        {
            var boxes = _boxApplicationService.GetBoxesByBoxWithChildren(boxFatherID);
            return View(boxes);
        }

        public IActionResult GenerateBoxes(int nivel)
        {
            _boxApplicationService.GenarateBoxes(nivel);
            return View("Index");
        }

        public IActionResult GenerateProductItems()
        {
            _productApplicationService.GenerateProductItem(new Guid("45458722-5D7C-48F9-AE8D-96CDC4B31CE8"), 5040);
            return View("Index");
        }
    }
}