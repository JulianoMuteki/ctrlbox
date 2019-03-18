using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class StockProduct : ValueObject<StockProduct>
    {
        public Guid StockID { get; set; }
        public Guid ProductID { get; set; }

        public virtual Stock Stock { get; set; }
        public virtual Product Product { get; set; }

        public int Amount { get; set; }

        protected override bool EqualsCore(StockProduct other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
