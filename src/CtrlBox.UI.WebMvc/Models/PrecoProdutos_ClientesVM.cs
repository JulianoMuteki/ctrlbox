using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
{
    public class PrecoProdutos_ClientesVM
    {
        public Guid ProdutoID { get; set; }

        public Guid ClienteID { get; set; }

        public double Preco { get; set; }

        public virtual ClienteVM Cliente { get; set; }

        public virtual ProdutoVM Produto { get; set; }
    }
}