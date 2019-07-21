using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Traceability : EntityBase
    {
        public Guid? ProductItemID { get; set; }
        public ProductItem ProductItem { get; set; }

        public Guid? BoxID { get; set; }
        public Box Box { get; set; }

        public Guid TraceTypeID { get; set; }
        public TraceType TraceType { get; set; }

        public ICollection<TraceabilityClient> TraceabilitiesClients { get; set; }

        public Traceability()
            : base()
        {
            this.TraceabilitiesClients = new HashSet<TraceabilityClient>();
        }

                public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }
    }
}
