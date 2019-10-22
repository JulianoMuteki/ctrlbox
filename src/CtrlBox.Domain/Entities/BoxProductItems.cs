using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class BoxProductItems : ValueObject<BoxProductItems>
    {
        public Box Box { get; private set; }
        public Product Product { get; private set; }

        public Guid ProductID { get; private set; }
        public Guid BoxID { get; private set; }

        public int TotalItems { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }


        private BoxProductItems()
        {

        }

        public static BoxProductItems FactoryCreate(Guid boxID, Guid productID)
        {
            return new BoxProductItems()
            {
                BoxID = boxID,
                ProductID = productID
            };
        }
    }
}
