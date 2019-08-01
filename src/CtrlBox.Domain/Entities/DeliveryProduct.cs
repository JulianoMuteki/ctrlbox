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

        public int QuantityProductItem { get; set; }

        public Delivery Delivery { get; set; }
        public Product Product { get; set; }

        public DeliveryProduct()
        {

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {

            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));

        }
    }
}
