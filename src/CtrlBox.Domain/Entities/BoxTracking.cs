using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class BoxTracking : EntityBase
    {
        public Guid? ProductItemID { get; set; }
        public ProductItem ProductItem { get; set; }

        public Guid? BoxID { get; set; }
        public Box Box { get; set; }

        public Guid TrackingTypeID { get; set; }
        public TrackingType TrackingType { get; set; }

        public ICollection<BoxTrackingClient> BoxesTrackingClients { get; set; }

        public BoxTracking()
            : base()
        {
            this.BoxesTrackingClients = new HashSet<BoxTrackingClient>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }

        public void AddClient(Guid clientID)
        {
            this.BoxesTrackingClients.Add(new BoxTrackingClient()
            {
                ClientID = clientID,
                BoxTrackingID = this.Id
            });
        }
    }
}
