using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class LoadBoxProductItem : ValueObject<LoadBoxProductItem>
    {
        public Guid LoadBoxID { get; set; }
        public Guid ProductItemID { get; set; }
        public LoadBox LoadBox { get; set; }
        public ProductItem ProductItem { get; set; }

        public LoadBoxProductItem()
        {
           
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}
