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
    public class MobileController : Controller
    {
        private readonly IBoxApplicationService _boxApplicationService;
        private readonly IProductApplicationService _productApplicationService;
        private readonly IClientApplicationService _clientService;
        private readonly ITrackingApplicationService _trackingService;

        public MobileController(IBoxApplicationService boxApplicationService, IProductApplicationService productApplicationService,
                             IClientApplicationService clientService, ITrackingApplicationService trackingService)
        {
            _boxApplicationService = boxApplicationService;
            _productApplicationService = productApplicationService;
            _trackingService = trackingService;
            _clientService = clientService;
        }


        public IActionResult BoxCreate()
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
        public IActionResult BoxCreate(BoxVM boxVM)
        {
            try
            {
                _boxApplicationService.Add(boxVM);
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
    }
}