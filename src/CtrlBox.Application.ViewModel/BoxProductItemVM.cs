using System;

namespace CtrlBox.Application.ViewModel
{
    public class BoxProductItemVM
    {
        public Guid BoxID { get; set; }
        public Guid ProductItemID { get; set; }
        public BoxVM Box { get; set; }
        public ProductItemVM ProductItem { get; set; }

        public bool IsDelivered { get; set; }
        public bool IsItemRemovedBox { get; set; }

        public Guid? OrderID { get; set; }
        public OrderVM Order { get; set; }
    }
}
