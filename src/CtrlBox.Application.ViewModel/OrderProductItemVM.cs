using System;

namespace CtrlBox.Application.ViewModel
{
    public class OrderProductItemVM
    {
        public Guid ProductItemID { get; set; }
        public ProductItemVM ProductItem { get; set; }

        public Guid OrderID { get; set; }
        public OrderVM Order { get; set; }
    }
}
