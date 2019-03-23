﻿using CtrlBox.UI.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace CtrlBox.UI.WebMvc.EndPoints
{
    public class WebApiClient<T> where T : ViewModelBase
    {
        HttpClient client = new HttpClient();

        private string _urlEndPoint = string.Empty;
        private string _controller = string.Empty;

        public WebApiClient(string urlEndPoint, string controller)
        {
            _urlEndPoint = urlEndPoint;
            _controller = controller;
            // Update port # in the following line.
            client.BaseAddress = new Uri(_urlEndPoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ICollection<T> GetT()
        {
            string action = $"{_urlEndPoint}/api/{_controller}";

            ICollection<T> T = null;
            HttpResponseMessage response = client.GetAsync(action).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<T>>().Result;
            }

            return T;
        }

        public T GetT(Guid id)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/{id.ToString()}";

            T productVM = null;
            HttpResponseMessage response = client.GetAsync(action).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<T>().Result;
            }

            return productVM;
        }

        public Uri Create(T product)
        {
            string action = $"{_urlEndPoint}/api/{_controller}";

            HttpResponseMessage response = client.PostAsJsonAsync(
              action, product).Result;
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public T Update(T product)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/{product.DT_RowId}";

            HttpResponseMessage response = client.PutAsJsonAsync(
                action, product).Result;
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = response.Content.ReadAsAsync<T>().Result;
            return product;
        }

        public HttpStatusCode Delete(Guid id)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/{id.ToString()}";

            HttpResponseMessage response = client.DeleteAsync(action).Result;
            return response.StatusCode;
        }
    }
}