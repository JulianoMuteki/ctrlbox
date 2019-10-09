using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CtrlBox.UI.Desktop.EndPoints
{
    public class WebApiMobile : WebApiBase<ClientVM>, IDisposable
    {
        public WebApiMobile(string urlEndPoint, string controller)
            : base(urlEndPoint, controller)
        {

        }

        public void Dispose()
        {
            base.DisposeBase();
        }

        ~WebApiMobile()
        {
            base.DisposeBase();
        }

        internal ICollection<RouteVM> GetRoutesAvailable(Guid userID)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/GetRoutesAvailable/{userID}";

            ICollection<RouteVM> T = null;
            HttpResponseMessage response = httpClient.GetAsync(action).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<RouteVM>>().Result;
            }

            return T;
        }
    }
}
