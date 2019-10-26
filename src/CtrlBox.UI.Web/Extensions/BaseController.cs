using CtrlBox.CrossCutting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

            ViewBag.Notifications = notifications;
        }
    }
}
