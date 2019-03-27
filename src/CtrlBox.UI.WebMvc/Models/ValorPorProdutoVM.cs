using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
{
    public class ValorPorProdutoVM
    {

        public ValorPorProdutoVM(ClientProductValueVM item, ProdutoVM produto)
        {
            this.DT_RowId = produto.DT_RowId.ToString();
            this.NomeProduto = produto.Name;
            this.Valor = item.Price.ToString();
        }
        public string DT_RowId { get; set; }
        public string NomeProduto { get; set; }
        public string Valor { get; set; }
    }
}