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
        private readonly IClientApplicationService _clientService;
        private readonly ITrackingApplicationService _trackingService;

        public BoxController(IBoxApplicationService boxApplicationService, IProductApplicationService productApplicationService,
                             IClientApplicationService clientService, ITrackingApplicationService trackingService)
        {
            _boxApplicationService = boxApplicationService;
            _productApplicationService = productApplicationService;
            _trackingService = trackingService;
            _clientService = clientService;
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
                var boxes = boxesVM.GroupBy(n => n.BoxTypeID)
                    .Select(g => new
                    {
                        DT_RowId = g.Key,
                        BoxType = g.Select(x => x.BoxType.Name).FirstOrDefault(),
                        SrcPicture = g.Select(x => x.BoxType.Picture.SrcBase64Image).FirstOrDefault(),
                        TotalBox = g.Count()
                    }
                    ).ToList();


                return Json(new
                {
                    aaData = boxes,
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

        public IActionResult BoxStock()
        {
            var boxesType = _boxApplicationService.GetAllBoxesType()
                                        .Select(boxType => new SelectListItem
                                        {
                                            Value = boxType.DT_RowId,
                                            Text = $"{boxType.Name} - {boxType.Description}"
                                        }).ToList();
            ViewData["BoxesType"] = boxesType;

            var clients = _clientService.GetAll()
                                        .Select(client => new SelectListItem
                                        {
                                            Value = client.DT_RowId,
                                            Text = client.Name
                                        }).ToList();
            ViewData["Clients"] = clients;

            var products = _productApplicationService.GetAll()
                                        .Select(prod => new SelectListItem
                                        {
                                            Value = prod.DT_RowId,
                                            Text = $"{prod.Name} - {prod.Description} - {prod.Package} - {prod.Capacity}{prod.UnitMeasure}"
                                        }).ToList();
            ViewData["Products"] = products;

            var trackingsTypes = _trackingService.GetAllTrackingsTypesByPlace()
                                        .Select(trace => new SelectListItem
                                        {
                                            Value = trace.DT_RowId,
                                            Text = trace.Description
                                        }).ToList();
            ViewData["TrackingTypes"] = trackingsTypes;
            return Index();
        }

        [HttpGet]
        public IActionResult GetAjaxHandlerAvailableProductItemsByClientIDAndProductID(Guid productID, Guid clientID)
        {
            try
            {
                var productItems = _productApplicationService.GetAvailableStockProductItemsByClientIDAndProductID(productID, clientID);
                return Json(new
                {
                    aaData = productItems,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult PostAjaxHandlerAddBoxStockWithProductItems(Guid boxTypeID, Guid trackingTypeID, Guid clientID, Guid productID, int quantity)
        {
            try
            {
                _productApplicationService.AddBoxStockWithProductItems(boxTypeID, trackingTypeID, clientID, productID, quantity);
                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult GenerateBoxes(int nivel)
        {
            _boxApplicationService.GenarateBoxes(nivel);
            return RedirectToAction("Index");
        }

        public IActionResult GenerateProductItems()
        {
            _productApplicationService.GenerateProductItem(new Guid("45458722-5D7C-48F9-AE8D-96CDC4B31CE8"), 5040);
            return RedirectToAction("Index");
        }
    }
}