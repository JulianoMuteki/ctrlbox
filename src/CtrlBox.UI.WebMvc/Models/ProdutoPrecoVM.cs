using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
{
    public class ProdutoPrecoVM
    {
        public string NomeProduto { get; set; }
        public string ValorProduto { get; set; }
        public string DT_RowId { get; set; }
        public int QtdeVenda { get; set; }
        public int QtdeRetorno { get; set; }
        /// <summary>
        /// Total de Valor produto X Qtde
        /// </summary>
        public string Total { get; set; }

    }
}