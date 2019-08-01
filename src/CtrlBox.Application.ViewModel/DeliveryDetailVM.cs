using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class DeliveryDetailVM
    {
        public Guid ProductID { get; set; }
        public ProductVM Product { get; set; }

        public int QuantityProductItem { get; set; }

        public Guid OrderID { get; set; }
        public OrderVM Order { get; set; }

        public Guid ClientID { get; set; }
        public ClientVM Client { get; set; }
        public ICollection<DeliveryBoxVM> DeliveriesBoxes { get; set; }

        public DeliveryDetailVM()
        {
            this.DeliveriesBoxes = new HashSet<DeliveryBoxVM>();
        }
    }
}
