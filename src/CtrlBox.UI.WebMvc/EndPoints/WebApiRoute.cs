using CtrlBox.UI.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CtrlBox.UI.WebMvc.EndPoints
{
    public class WebApiRoute : WebApiBase<RouteVM>
    {
        public WebApiRoute(string urlEndPoint, string controller)
            : base(urlEndPoint, controller)
        {

        }

        internal object ConnectRouteToClient(RouteVM entity)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/ConnectRouteToClient";

            HttpResponseMessage response = httpClient.PostAsJsonAsync(
              action, entity).Result;
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
    }
}