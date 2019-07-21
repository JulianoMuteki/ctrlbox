using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class TraceabilityVM : ViewModelBase
    {
        public Guid? ProductItemID { get; set; }
        public ProductItemVM ProductItem { get; set; }

        public Guid? BoxID { get; set; }
        public BoxVM Box { get; set; }

        public Guid TraceTypeID { get; set; }
        public TraceTypeVM TraceType { get; set; }

        public ICollection<TraceabilityClient> TraceabilitiesClients { get; set; }
    }
}
