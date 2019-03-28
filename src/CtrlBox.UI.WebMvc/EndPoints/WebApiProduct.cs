using CtrlBox.UI.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CtrlBox.UI.WebMvc.EndPoints
{
    public class WebApiProduct : WebApiBase<ProdutoVM>
    {
        public WebApiProduct(string urlEndPoint, string controller)
            : base(urlEndPoint, controller)
        {

        }

        internal object ConnectProductToClient(ICollection<ClientProductValueVM> productsClients)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/ConnectProductToClient";

            HttpResponseMessage response = httpClient.PostAsJsonAsync(
              action, productsClients).Result;
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        internal object AddProductStock(ICollection<StockProductVM> stocksProductsVM)
        {
            string action = $"{_urlEndPoint}/api/{_controller}/AddProductStock";

            HttpResponseMessage response = httpClient.PostAsJsonAsync(
              action, stocksProductsVM).Result;
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
    }
}