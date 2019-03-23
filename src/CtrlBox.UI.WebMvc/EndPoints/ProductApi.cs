using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CtrlBox.UI.WebMvc.Models;

namespace CtrlBox.UI.WebMvc.EndPoints
{
    public class ProdutoVMApi
    {
        HttpClient client = new HttpClient();

        private string _urlEndPoint = string.Empty;

        public ProdutoVMApi(string urlEndPoint)
        {
            _urlEndPoint = urlEndPoint;
            // Update port # in the following line.
            client.BaseAddress = new Uri(_urlEndPoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ICollection<ProdutoVM> GetProdutoVM()
        {
            string path = "";
            ICollection<ProdutoVM> ProdutoVM = null;
            HttpResponseMessage response = client.GetAsync(_urlEndPoint + path).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<ProdutoVM>>().Result;
            }

            return ProdutoVM;
        }

        public ProdutoVM GetProdutoVM(Guid id)
        {
            string path = "";

            ProdutoVM productVM = null;
            HttpResponseMessage response = client.GetAsync(_urlEndPoint + path + "/" + id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ProdutoVM>().Result;
            }

            return productVM;
        }

        public Uri CreateProduct(ProdutoVM product)
        {
            HttpResponseMessage response = client.PostAsJsonAsync(
                "api/Product", product).Result;
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public ProdutoVM UpdateProductAsync(ProdutoVM product)
        {
            HttpResponseMessage response = client.PutAsJsonAsync(
                $"api/products/{product.DT_RowId}", product).Result;
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = response.Content.ReadAsAsync<ProdutoVM>().Result;
            return product;
        }

        public HttpStatusCode DeleteProductAsync(Guid id)
        {
            HttpResponseMessage response = client.DeleteAsync(
                $"api/products/{id}").Result;
            return response.StatusCode;
        }
    }
}