using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class OrderBox : ValueObject<OrderBox>
    {
        public Guid BoxID { get; set; }
        public Box Box { get; set; }

        public Guid OrderID { get; set; }
        public Order Order { get; set; }

        private OrderBox()
        {

        }

        public static OrderBox FactoryCreate(Guid orderID, Guid boxID)
        {
            return new OrderBox()
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
