using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
{
    public class ValorPorProdutoVM
    {

        public ValorPorProdutoVM(PrecoProdutos_Clientes item, ProdutoVM produto)
        {
            //this.DT_RowId = produto.ProdutoID.ToString();
            //this.NomeProduto = produto.Nome;
            //this.Valor = item.Preco.ToString();
        }
        public string DT_RowId { get; set; }
        public string NomeProduto { get; set; }
        public string Valor { get; set; }
    }

    public class PrecoProdutos_Clientes
    {
    }
}