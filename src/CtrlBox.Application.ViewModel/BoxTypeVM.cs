using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class BoxTypeVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int QuantityToDelivery { get; set; }

        public ICollection<BoxVM> Boxes { get; set; }

        public BoxTypeVM()
        : base()
        {
            this.Boxes = new HashSet<BoxVM>();
        }
    }
}
