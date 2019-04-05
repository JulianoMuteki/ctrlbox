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

        public Product Product { get; set; }
        public Sale Sale { get; set; }

        public SaleProduct()
        {

        }
    }
}
