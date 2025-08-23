using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using CtrlBox.Application.ViewModel;

namespace CtrlBox.UI.Desktop.EndPoints
{
    public abstract class WebApiBase<T> where T : ViewModelBase
    {
        protected HttpClient httpClient = new HttpClient();

        protected string _urlEndPoint = string.Empty;
        protected string _controller = string.Empty;

        public WebApiBase(string urlEndPoint, string controller)
        {
            _urlEndPoint = urlEndPoint;
            _controller = controller;
            // Update port # in the following line.
            httpClient.BaseAddress = new Uri(_urlEndPoint);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ICollection<T> GetT()
        {
            string action = $"{_urlEndPoint}/api/{_controller}";

            ICollection<T> T = null;
            HttpResponseMessage response = httpClient.GetAsync(action).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<T>>().Result;
            }

            return T;
        }

        public T GetT(Guid id)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/{id.ToString()}";

            T entityVM = null;
            HttpResponseMessage response = httpClient.GetAsync(action).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<T>().Result;
            }

            return entityVM;
        }

        public Uri Create(T entity)
        {
            string action = $"{_urlEndPoint}/api/{_controller}";

            HttpResponseMessage response = httpClient.PostAsJsonAsync(
              action, entity).Result;
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public T Update(T entity)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/{entity.DT_RowId}";

            HttpResponseMessage response = httpClient.PutAsJsonAsync(
                action, entity).Result;
            response.EnsureSuccessStatusCode();

            // Deserialize the updated entity from the response body.
            entity = response.Content.ReadAsAsync<T>().Result;
            return entity;
        }

        public HttpStatusCode Delete(Guid id)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/{id.ToString()}";

            HttpResponseMessage response = httpClient.DeleteAsync(action).Result;
            return response.StatusCode;
        }

        public void DisposeBase()
        {
            httpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
