using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
{
    public class ProdutoVM: ViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
    }
}