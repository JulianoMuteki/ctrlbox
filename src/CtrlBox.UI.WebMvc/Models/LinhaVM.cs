using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
{
    public class LinhaVM
    {
       

        public LinhaVM(LinhaVM item)
        {
            this.DT_RowId = item.DT_RowId.ToString();
            this.Nome = item.Nome;
            this.DistanciaKM = item.DistanciaKM.ToString();
            this.Caminhao = item.Caminhao;
            this.Tempo = item.Tempo.ToString();
        }

        public LinhaVM()
        {
            this.DT_RowId = Guid.Empty.ToString();
        }
        public string DT_RowId { get; set; }
        public string Nome { get; set; }
        public string DistanciaKM { get; set; }
        public string Tempo { get; set; }
        public string Caminhao { get; set; }

        public bool ExisteEntregaAberta { get; set; }
    }
}