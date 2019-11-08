using System;
using System.Linq;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Extensions;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CtrlBox.UI.Web.Controllers
{
    public class MobileController : BaseController
    {
        private readonly IBoxApplicationService _boxAppService;
        private readonly IProductApplicationService _productApplicationService;
        private readonly IClientApplicationService _clientService;
        private readonly ITrackingApplicationService _trackingService;

        public MobileController(NotificationContext notificationContext, IBoxApplicationService boxApplicationService, IProductApplicationService productApplicationService,
                             IClientApplicationService clientService, ITrackingApplicationService trackingService)
            : base(notificationContext)
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
            try
            {
                var barcodes = _boxAppService.GetListBarcodes(quantity);
                return Json(new
                {
                    aaData = barcodes,
                    success = true
                });
            }
            catch (Exception ex)
            {
                return Json(CustomExceptionHandler.AjaxException(ex, Response));
            }
        }

        [HttpPost]
        public ActionResult PostAjaxHandlerCreateBox(string entity)
        {
            try
            {
                JsonSerialize jsonS = new JsonSerialize();
                var createBoxVM = jsonS.JsonDeserializeObject<CreateBoxVM>(entity);
                _boxAppService.Add(createBoxVM);

                if (_notificationContext.HasNotifications)
                {
                    return GetJSONResultNotifications();
                }

                return GetJSONResultOK();
            }
            catch (Exception ex)
            {
                return Json(CustomExceptionHandler.AjaxException(ex, Response));
            }
        }
    }
}