using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
{
    public class StockProductVM
    {
        public int Amount { get; set; }
        public Guid ProductID { get; set; }
        public Guid StockID { get; set; }

        public string Name { get; set; }
    }
}