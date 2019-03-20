using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDE.UI.Portal.Models
{
    public class CadastroVenda
    {
        public IList<ProdutoPrecoVM> ProdutosPrecos { get; set; }

        public double SaldoAnterior { get; set; }
        public int CaixasEmDebito { get; set; }

    }
}