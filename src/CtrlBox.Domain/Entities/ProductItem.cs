using CtrlBox.Domain.Common;
using System;

namespace CtrlBox.Domain.Entities
{
    public class ProductItem: EntityBase
    {
        public string Barcode { get; set; }
        public string Weight { get; set; }

        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        public ProductItem()
            :base()
        {

        }
    }
}
