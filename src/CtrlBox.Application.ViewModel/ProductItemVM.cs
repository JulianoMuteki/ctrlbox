using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class ProductItemVM : ViewModelBase
    {
        public string Barcode { get; set; }
        public string Status { get; set; }

        public Guid ProductID { get; set; }
        public ProductVM Product { get; set; }

        public ICollection<BoxProductItemVM> BoxesProductItems { get; set; }

        public ProductItemVM()
            : base()
        {
            this.BoxesProductItems = new HashSet<BoxProductItemVM>();
        }
    }
}
