using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void SubtractProductsDelivered(int amount)
        {
            this.Amount -= amount;
            if (this.Amount < 0)
                throw new Exception("Not enough stock!");
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {

            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));

        }
    }
}
