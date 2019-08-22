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

        public bool IsItemRemovedBox { get; set; }

        private BoxProductItem()
        {
           
        }

        public static BoxProductItem FactoryCreate(Guid boxID, Guid productItemID)
        {
            return new BoxProductItem()
            {
                BoxID = boxID,
                ProductItemID = productItemID
            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }

        public void Deliver()
        {
            this.ProductItem.Deliver();
        }

        public void AddTrackingProductItem(Guid trackingTypeID, Guid clientID)
        {
            this.ProductItem.AddTracking(trackingTypeID, clientID);
        }

        internal void FinishDelivery(bool hasCrossDocking)
        {
            this.ProductItem.FinishDelivery(hasCrossDocking);
        }
    }
}
