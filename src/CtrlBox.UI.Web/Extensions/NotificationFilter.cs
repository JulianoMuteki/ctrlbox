using CtrlBox.CrossCutting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CtrlBox.UI.Web.Extensions
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext filterContext, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasNotifications)
            {

                // do something after the action executes
                ViewResult result = filterContext.Result as ViewResult;
                if (result != null)
                {
                    result.ViewData["ViewData_NOTIFICATION"] =
                    "Comes from MyActionAttributeFilter at " + DateTime.Now.ToLongTimeString();
                }

                //var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);

                //var controller = filterContext.Controller as Controller;
                //if (controller != null && filterContext.ModelState != null)
                //{
                //    // var modelState = ModelStateHelpers.SerialiseModelState(filterContext.ModelState);
                //    controller.TempData["KEY_NOTIFICATION"] = notifications;
                //    controller.ViewData["ViewData_NOTIFICATION"] = notifications;
                //}

                return;
            }

            await next();
        }

    }
}
