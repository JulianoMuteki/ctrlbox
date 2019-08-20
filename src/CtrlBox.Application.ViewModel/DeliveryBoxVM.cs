using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class DeliveryBoxVM
    {
        public Guid BoxID { get; set; }
        public BoxVM Box { get; set; }

        public Guid DeliveryDetailID { get; set; }
        public DeliveryDetailVM DeliveryDetail { get; set; }

    }
}
