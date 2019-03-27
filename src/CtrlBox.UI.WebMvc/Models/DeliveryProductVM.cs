using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
{
    public class DeliveryProductVM
    {
        public Guid DeliveryID { get; set; }
        public Guid ProductID { get; set; }

        public int Amount { get; set; }
    }
}