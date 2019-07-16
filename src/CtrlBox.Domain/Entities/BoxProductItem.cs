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

        public BoxProductItem()
        {
           
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}
