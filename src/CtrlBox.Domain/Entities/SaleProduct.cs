using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class SaleProduct : ValueObject<SaleProduct>
    {
        public int Amount { get; set; }
        public decimal SaleValue { get; set; }
        public int ExchangeQuantity { get; set; }

        public Guid SaleID { get; set; }
        public Guid ProductID { get; set; }

        public virtual Product Product { get; set; }
        public virtual Sale Sale { get; set; }

        protected override bool EqualsCore(SaleProduct other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
