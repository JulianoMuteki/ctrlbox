using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class SaleProductVM
    {
        public int Amount { get; set; }
        public decimal SaleValue { get; set; }
        public int DiscountAmount { get; set; }

        public Guid SaleID { get; set; }
        public Guid ProductID { get; set; }

        public virtual ProductVM Product { get; set; }
        public virtual SaleVM Sale { get; set; }
    }
}
