using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class StockProduct : ValueObject<StockProduct>
    {
        public Guid StockID { get; set; }
        public Guid ProductID { get; set; }

        public Stock Stock { get; set; }
        public Product Product { get; set; }

        public int Amount { get; set; }

        public StockProduct()
        {
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {

            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));

        }
    }
}
