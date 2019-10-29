using CtrlBox.CrossCutting;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq;

namespace CtrlBox.UI.Web.Extensions
{
    public class BaseController: Controller
    {
        private readonly IProductApplicationService _productService;
        protected readonly NotificationContext _notificationContext;

        public BaseController(NotificationContext notificationContext, IProductApplicationService productService)
        {
            _notificationContext = notificationContext;
            _productService = productService;
        }

        public void PushNotification()
        {
            var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);
            ViewBag.Notifications = notifications;
        }

        public void LoadViewDataProducts()
        {
            var products = _productService.GetAll()
                .Select(prod => new SelectListItem
                {
                    Value = prod.DT_RowId,
                    Text = $"{prod.Name} - {prod.Description} - {prod.Package} - {prod.Capacity}{prod.UnitMeasure}"
                }).ToList();
            ViewData["Products"] = products;
        }   
    }
}
