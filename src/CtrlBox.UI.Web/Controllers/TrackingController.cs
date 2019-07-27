using System;
using System.Linq;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CtrlBox.UI.Web.Controllers
{
    public class TrackingController : Controller
    {
        private readonly IBoxTrackingApplicationService _boxTrackingApplicationService;
        private readonly IBoxApplicationService _boxApplicationService;
        private readonly IProductApplicationService _productApplicationService;
        private readonly IClientApplicationService _clientApplicationService;

        public TrackingController(IBoxTrackingApplicationService boxTrackingApplicationService, IBoxApplicationService boxApplicationService,
                                IProductApplicationService productApplicationService, IClientApplicationService clientApplicationService)
        {
            _boxApplicationService = boxApplicationService;
            _productApplicationService = productApplicationService;
            _boxTrackingApplicationService = boxTrackingApplicationService;
            _clientApplicationService = clientApplicationService;
        }

        public IActionResult Index(Guid boxID)
        {
            var boxVM = _boxApplicationService.GetBoxesByIDWithBoxTypeAndProductItems(boxID);
            ViewData["BoxViewData"] = boxVM;

            var traces = _boxTrackingApplicationService.GetByBoxID(boxID);
            return View(traces);
        }

        public IActionResult Create()
        {
            var boxes = _boxApplicationService.GetAll()
                                           .Select(box => new SelectListItem
                                           {
                                               Value = box.DT_RowId,
                                               Text = $"{box.BoxBarcode.BarcodeEAN13} - {box.Description }"
                                           }).ToList();

            var productsItems = _productApplicationService.GetProductsItems()
                                        .Select(prod => new SelectListItem
                                        {
                                            Value = prod.DT_RowId,
                                            Text = $"{prod.Barcode} - {prod.Product.Name }"
                                        }).ToList();

            var trackingsTypes = _boxTrackingApplicationService.GetAllTrackingsTypes()
                            .Select(trace => new SelectListItem
                            {
                                Value = trace.DT_RowId,
                                Text = trace.Description
                            }).ToList();

            var clients = _clientApplicationService.GetAll()
                .Select(client => new SelectListItem
                {
                    Value = client.DT_RowId,
                    Text = client.Name
                }).ToList();

            ViewData["Clients"] = clients;
            ViewData["Boxes"] = boxes;
            ViewData["ProductsItems"] = productsItems;
            ViewData["TrackingTypes"] = trackingsTypes;

            return View();
        }

        [HttpPost]
        public IActionResult Create(BoxTrackingVM traceabilityVM)
        {
            try
            {
                _boxTrackingApplicationService.Add(traceabilityVM);
                return RedirectToAction("Index", "Box");
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

        public IActionResult TrackingTypes()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAjaxHandlerTrackingTypes()
        {
            try
            {
                var tracesTypes = _boxTrackingApplicationService.GetAllTrackingsTypes();

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

        public IActionResult CreateTrackingType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTrackingTypes(TrackingTypeVM trackingTypeVM, IFormFile FilePicture)
        {
            try
            {
                PictureVM imageEntity = GeneratePicture.CreatePicture(FilePicture, $"{trackingTypeVM.Description}");
                trackingTypeVM.Picture = imageEntity;

                _boxTrackingApplicationService.AddTraceType(trackingTypeVM);
                return RedirectToAction("TrackingTypes");
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

        [HttpGet]
        public IActionResult GetAjaxHandlerTrackingType(string trackingTypeID)
        {
            try
            {
                var trackingTypes = _boxTrackingApplicationService.GetTrackTypeById(new Guid(trackingTypeID));

                return Json(new
                {
                    aaData = trackingTypes,
                    success = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}