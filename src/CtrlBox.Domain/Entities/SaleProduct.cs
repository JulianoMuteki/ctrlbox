﻿using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class SaleProduct : ValueObject<SaleProduct>
    {
        public int Amount { get; set; }
        public decimal SaleValue { get; set; }
        public int DiscountAmount { get; set; }

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
    }
}
