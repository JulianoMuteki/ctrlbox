using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class SaleProduct : ValueObject<SaleProduct>
    {
        public int Quantity { get; set; }
        public decimal ValueProductSale { get; set; }
        public int DiscountValueSale { get; set; }
        public decimal TotalValue { get; set; }

        public Guid SaleID { get; set; }
        public Guid ProductID { get; set; }

        public Product Product { get; set; }
        public Sale Sale { get; set; }

        public SaleProduct()
        {

        }
        protected override IEnumerable<object> GetEqualityComponents()
        {

            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));

        }

        public void CalcTotalValue()
        {
            this.TotalValue = (Quantity * ValueProductSale) - DiscountValueSale;
        }
    }
}
