using CDE.UI.Portal.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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


       public async Task<ProdutoVM> GetProdutoVMAsync(string path)
        {
            ProdutoVM ProdutoVM = null;
            HttpResponseMessage response = await client.GetAsync(_urlEndPoint + path);
            if (response.IsSuccessStatusCode)
            {
                ProdutoVM = await response.Content.ReadAsAsync<ProdutoVM>();
            }

            ShowProdutoVM(ProdutoVM);
            return ProdutoVM;
        }


    }
}