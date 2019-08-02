using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Application.ViewModel
{
    public class BoxVM: ViewModelBase
    {
        public string Description { get; set; }
        public string Status { get; set; }

        public int PorcentFull { get; set; }

        public Guid BoxTypeID { get; set; }
        public BoxTypeVM BoxType { get; set; }

        public Guid? BoxParentID { get; set; }
        public BoxVM BoxParent { get; set; }

        public Guid? ProductID { get; set; }
        public ProductVM Product { get; set; }

        public int RangeProductsItems { get; set; }
        public BoxBarcodeVM BoxBarcode { get; set; }

        public string[] ChildrenBoxesID { get; set; }

        private int _totalProductsItemsChildren;

        public int TotalProductsItemsChildren
        {
            get
            {
                return SumTotalProductItemsChildren();
            }
        }


        public ICollection<BoxVM> BoxesChildren { get; set; }
        public ICollection<BoxProductItemVM> BoxesProductItems { get; set; }
        public ICollection<OrderBoxVM> OrdersBoxes { get; set; }
        public ICollection<DeliveryBoxVM> DeliveriesBoxes { get; set; }
        public ICollection<TrackingVM> Trackings { get; set; }

        public BoxVM()
            : base()
        {
            this.DeliveriesBoxes = new HashSet<DeliveryBoxVM>();
            this.Trackings = new HashSet<TrackingVM>();
            this.BoxesChildren = new HashSet<BoxVM>();
            this.BoxesProductItems = new HashSet<BoxProductItemVM>();
            this.OrdersBoxes = new HashSet<OrderBoxVM>();
        }

        private int SumTotalProductItemsChildren()
        {
            if (BoxesProductItems.Count > 0)
                _totalProductsItemsChildren = BoxesProductItems.Where(p => p.IsDelivered == false).Count();
            else
                _totalProductsItemsChildren += this.BoxesChildren.Sum(x => x.TotalProductsItemsChildren);

            return _totalProductsItemsChildren;
        }

    }
}
