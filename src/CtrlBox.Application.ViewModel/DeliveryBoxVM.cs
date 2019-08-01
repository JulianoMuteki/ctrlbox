using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class DeliveryBoxVM
    {
        public Guid BoxID { get; set; }
        public Guid DeliveryID { get; set; }
        public Guid BoxTypeID { get; set; }

        public OrderVM Delivery { get; set; }
        public BoxVM Box { get; set; }

    }
}
