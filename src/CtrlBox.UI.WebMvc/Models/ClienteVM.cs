using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDE.UI.Portal.Models
{
    public class ClienteVM
    {
    public string DT_RowId { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public int TotalCaixa { get; set; }
        public double SaldoDevedor { get; set; }

        public bool StatusEntrega { get; set; }

        public ClienteVM()
        {

        }

        public ClienteVM(ClienteVM cliente)
        {
            this.Nome = cliente.Nome;
            this.DT_RowId = cliente.DT_RowId.ToString();
           

        }
    }
}