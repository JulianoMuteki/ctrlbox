using CtrlBox.UI.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CtrlBox.UI.WebMvc.EndPoints
{
    public class WebApiClient : WebApiBase<ClienteVM>
    {
        public WebApiClient(string urlEndPoint, string controller)
            : base(urlEndPoint, controller)
        {

        }

        public ICollection<ClienteVM> GetAvailable(Guid idRoute)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/GetAvailable/{idRoute}";

            ICollection<ClienteVM> T = null;
            HttpResponseMessage response = httpClient.GetAsync(action).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<ClienteVM>>().Result;
            }

            return T;
        }

        internal ICollection<ClienteVM> GetNotAvailable(Guid idRoute)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/GetNotAvailable/{idRoute}";

            ICollection<ClienteVM> T = null;
            HttpResponseMessage response = httpClient.GetAsync(action).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<ClienteVM>>().Result;
            }

            return T;
        }

    }
}