using CtrlBox.Domain.Common;
using System;

namespace CtrlBox.Domain.Entities
{
    public class DeliveryProduct : ValueObject<DeliveryProduct>
    {
        public Guid DeliveryID { get; set; }
        public Guid ProductID { get; set; }

        public int Amount { get; set; }

        public Delivery Delivery { get; set; }
        public Product Product { get; set; }

        public DeliveryProduct()
        {

        }
    }
}
