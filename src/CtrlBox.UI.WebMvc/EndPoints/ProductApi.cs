using System;
using System.Collections.Generic;
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

       public void ShowProdutoVM(ProdutoVM ProdutoVM)
        {
            Console.WriteLine($"Name: {ProdutoVM.Nome}\tDT_ROWID: " +
                $"{ProdutoVM.DT_RowId}\tQTDE: {ProdutoVM.Qtde}");
        }


       public async Task<ICollection<ProdutoVM>> GetProdutoVMAsync(string path)
        {
            ICollection<ProdutoVM> ProdutoVM = null;
            HttpResponseMessage response = await client.GetAsync(_urlEndPoint + path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ICollection<ProdutoVM>>();
            }

            return ProdutoVM;
        }

        public ICollection<ProdutoVM> GetProdutoVM(string path)
        {
            ICollection<ProdutoVM> ProdutoVM = null;
            HttpResponseMessage response = client.GetAsync(_urlEndPoint + path).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<ProdutoVM>>().Result;
            }

            return ProdutoVM;
        }



    }
}