using System;

namespace CtrlBox.Application.ViewModel
{
    public class OrderBoxVM
    {
        public Guid BoxID { get; set; }
        public BoxVM Box { get; set; }

        public Guid OrderID { get; set; }
        public OrderVM Order { get; set; }

    }
}
