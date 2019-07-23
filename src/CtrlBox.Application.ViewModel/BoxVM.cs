using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Application.ViewModel
{
    public class BoxVM: ViewModelBase
    {
        public string Description { get; set; }
        public int StatusBox { get; set; }

        public float Lenght { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public string LengthUnit { get; set; }

        public int MaxProductsItems { get; set; }
        public int PorcentFull { get; set; }

        public Guid BoxTypeID { get; set; }
        public BoxTypeVM BoxType { get; set; }

        public Guid? BoxParentID { get; set; }
        public BoxVM BoxParent { get; set; }

        public Guid? ProductID { get; set; }
        public ProductVM Product { get; set; }

        public int RangeProductsItems { get; set; }
        public BoxBarcodeVM BoxBarcode { get; set; }

        public ICollection<BoxVM> BoxesChildren { get; set; }
        public string[] ChildrenBoxesID { get; set; }

        private int _totalProductsItemsChildren;

        public int TotalProductsItemsChildren
        {
            get
            {
                return SumTotalProductItemsChildren();
            }
        }


        public ICollection<BoxProductItemVM> BoxesProductItems { get; set; }
        public ICollection<DeliveryBoxVM> DeliveriesBoxes { get; set; }

        public BoxVM()
            : base()
        {
            this.BoxesChildren = new HashSet<BoxVM>();
            this.BoxesProductItems = new HashSet<BoxProductItemVM>();
            this.DeliveriesBoxes = new HashSet<DeliveryBoxVM>();
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
