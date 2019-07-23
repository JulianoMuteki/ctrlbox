using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class BoxTypeVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public float Lenght { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public string LengthUnit { get; set; }

        public int MaxProductsItems { get; set; }

        public int QuantityToDelivery { get; set; }

        public Guid? PictureID { get; set; }
        public PictureVM Picture { get; set; }

        public ICollection<BoxVM> Boxes { get; set; }

        public BoxTypeVM()
        : base()
        {
            this.Boxes = new HashSet<BoxVM>();
        }
    }
}
