using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class BoxVM: ViewModelBase
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int StatusBox { get; set; }

        public Guid BoxTypeID { get; set; }
        public BoxTypeVM BoxType { get; set; }

        public Guid? BoxParentID { get; set; }
        public BoxVM BoxParent { get; set; }

        public Guid? ProductID { get; set; }
        public ProductVM Product { get; set; }

        public int RangeProductsItems { get; set; }
        
        public ICollection<BoxVM> BoxesChildren { get; set; }
        public string[] ChildrenBoxesID { get; set; }

        public ICollection<BoxProductItemVM> BoxesProductItems { get; set; }
        public ICollection<DeliveryBoxVM> DeliveriesBoxes { get; set; }

        public BoxVM()
            : base()
        {
            this.BoxesChildren = new HashSet<BoxVM>();
            this.BoxesProductItems = new HashSet<BoxProductItemVM>();
            this.DeliveriesBoxes = new HashSet<DeliveryBoxVM>();
        }

    }
}
