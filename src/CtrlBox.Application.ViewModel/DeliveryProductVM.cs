using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class DeliveryProductVM
    {
        public Guid DeliveryID { get; set; }
        public Guid ProductID { get; set; }

        public int Amount { get; set; }

        public DeliveryVM DeliveryVM { get; set; }
        public ProductVM ProductVM { get; set; }
    }
}
