using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class BoxProductItem : ValueObject<BoxProductItem>
    {
        public Guid BoxID { get; set; }
        public Guid ProductItemID { get; set; }
        public Box Box { get; set; }
        public ProductItem ProductItem { get; set; }

        public bool IsDelivered { get; set; }

        public Guid? DeliveryID { get; set; }
        public Delivery Delivery { get; set; }

        public BoxProductItem()
        {
           
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }

        public void Deliver()
        {
            this.IsDelivered = true;
            this.ProductItem.Deliver();
        }
    }
}
