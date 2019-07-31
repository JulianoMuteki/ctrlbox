using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Tracking : EntityBase
    {
        public Guid? ProductItemID { get; set; }
        public ProductItem ProductItem { get; set; }

        public Guid? BoxID { get; set; }
        public Box Box { get; set; }

        public Guid TrackingTypeID { get; set; }
        public TrackingType TrackingType { get; set; }

        public ICollection<TrackingClient> TrackingsClients { get; set; }

        public Tracking()
            : base()
        {
            this.TrackingsClients = new HashSet<TrackingClient>();
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
            this.TrackingsClients.Add(new TrackingClient()
            {
                ClientID = clientID,
                TrackingID = this.Id
            });
        }
    }
}
