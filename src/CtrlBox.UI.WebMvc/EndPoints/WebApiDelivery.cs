using CtrlBox.UI.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CtrlBox.UI.WebMvc.EndPoints
{
    public class WebApiDelivery : WebApiBase<DeliveryVM>
    {
        public WebApiDelivery(string urlEndPoint, string controller)
            : base(urlEndPoint, controller)
        {

        }
    }
}