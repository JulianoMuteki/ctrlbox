using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class ShipmentBox : ValueObject<ShipmentBox>
    {
        public Guid BoxID { get; set; }
        public Guid OrderID { get; set; }

        public Box Box { get; set; }
        public Order Order { get; set; }

        private ShipmentBox()
        {

        }

        public static ShipmentBox FactoryCreate(Guid orderID, Guid boxID)
        {
            return new ShipmentBox()
            {
                OrderID = orderID,
                BoxID = boxID
            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}
