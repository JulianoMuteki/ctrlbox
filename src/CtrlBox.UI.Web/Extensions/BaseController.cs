using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.UI.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.UI.Web.Extensions
{
    public class BaseController: Controller
    {
        protected readonly NotificationContext _notificationContext;

        public BaseController(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public void PushNotification()
        {
            var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);
        }

        public IActionResult GetPartialViewNotifications(string notifications)
        {
            JsonSerialize jsonS = new JsonSerialize();
            var lista = jsonS.JsonDeserializeObject<List<Notification>>(notifications);
            ViewData["Notifications"] = lista;

            return PartialView("~/Views/Modals/_ViewPartialNotifications.cshtml");
        }

        protected ActionResult GetJSONResultNotifications()
        {
            return Json(new
            {
                aaData = "OK",
                success = true,
                HasNotification = true,
                notificationsJSON = JsonConvert.SerializeObject(_notificationContext.Notifications)
        });
        }

        protected ActionResult GetJSONResultOK()
        {
            return Json(new
            {
                aaData = "OK",
                success = true,
                notification = false
            });
        }

        public void LoadViewDataProducts(IProductApplicationService productAppService)
        {
            var products = productAppService.GetAll()
                .Select(prod => new SelectListItem
                {
                    Value = prod.DT_RowId,
                    Text = $"{prod.Name} - {prod.Description} - {prod.Package} - {prod.Capacity}{prod.UnitMeasure}"
                }).ToList();
            ViewData["Products"] = products;
        }

        public void LoadViewDataClients(IClientApplicationService clientAppService)
        {
            var clients = clientAppService.GetAll()
                                        .Select(client => new SelectListItem
                                        {
                                            Value = client.DT_RowId,
                                            Text = client.Name
                                        }).ToList();
            ViewData["Clients"] = clients;
        }

        public void LoadViewDataTracking(ITrackingApplicationService trackingAppService)
        {
            var trackingsTypes = trackingAppService.GetAllTrackingsTypesByPlace()
                .Select(trace => new SelectListItem
                {
                    Value = trace.DT_RowId,
                    Text = trace.Description
                }).ToList();
            ViewData["TrackingTypes"] = trackingsTypes;
        }
    }
}
