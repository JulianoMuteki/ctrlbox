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

        public OrderBox()
        {

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}
