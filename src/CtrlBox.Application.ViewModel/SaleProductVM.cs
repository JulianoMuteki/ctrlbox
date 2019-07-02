using System;

namespace CtrlBox.Application.ViewModel
{
    public class SaleProductVM
    {
        public int Quantity { get; set; }
        public decimal ValueProductSale { get; set; }
        public int DiscountValueSale { get; set; }
        public decimal TotalValue { get; set; }

        public Guid SaleID { get; set; }
        public Guid ProductID { get; set; }

        public virtual ProductVM Product { get; set; }
        public virtual SaleVM Sale { get; set; }
    }
}
