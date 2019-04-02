using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CtrlBox.UI.WebMvc.EndPoints
{
    public class WebApiClient : WebApiBase<ClientVM>
    {
        public WebApiClient(string urlEndPoint, string controller)
            : base(urlEndPoint, controller)
        {

        }

        public ICollection<ClientVM> GetAvailable(Guid idRoute)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/GetAvailable/{idRoute}";

            ICollection<ClientVM> T = null;
            HttpResponseMessage response = httpClient.GetAsync(action).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<ClientVM>>().Result;
            }

            return T;
        }

        internal ICollection<ClientVM> GetNotAvailable(Guid idRoute)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/GetNotAvailable/{idRoute}";

            ICollection<ClientVM> T = null;
            HttpResponseMessage response = httpClient.GetAsync(action).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<ClientVM>>().Result;
            }

            return T;
        }

    }
}