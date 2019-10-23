using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CtrlBox.UI.Web.Controllers
{
    public class MobileController : Controller
    {
        private readonly IBoxApplicationService _boxAppService;
        private readonly IProductApplicationService _productApplicationService;
        private readonly IClientApplicationService _clientService;
        private readonly ITrackingApplicationService _trackingService;

        public MobileController(IBoxApplicationService boxApplicationService, IProductApplicationService productApplicationService,
                             IClientApplicationService clientService, ITrackingApplicationService trackingService)
        {
            _boxAppService = boxApplicationService;
            _productApplicationService = productApplicationService;
            _trackingService = trackingService;
            _clientService = clientService;
        }


        public IActionResult BoxCreate()
        {
            var boxesType = _boxAppService.GetAllBoxesType()
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

            var boxes = _boxAppService.GetBoxesParentsWithBoxTypeEndProduct()
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
                _boxAppService.Add(boxVM);
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

        [HttpGet]
        public ActionResult GetAjaxHandlerListBarcodes(int quantity)
        {
            var barcodes = _boxAppService.GetListBarcodes(quantity);
            return Json(new
            {
                aaData = barcodes,
                success = true
            });
        }


        [HttpPost]
        public ActionResult PostAjaxHandlerCreateBox(string entity)
        {
            JsonSerialize jsonS = new JsonSerialize();
            var createBoxVM = jsonS.JsonDeserializeObject<CreateBoxVM>(entity);
            _boxAppService.Add(createBoxVM);
            return Json(new
            {
                aaData = "OK",
                success = true
            });
        }
    }
}