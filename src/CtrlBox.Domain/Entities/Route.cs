using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class Route : EntityBase
    {
        public string Name { get; set; }
        public int KmDistance { get; set; }
        public string Truck { get; set; }
        public bool HasOpenDelivery { get; set; }

        public Guid ClientOriginID { get; set; }
        public Client ClientOrigin { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }
        public ICollection<RouteClient> RoutesClients { get; set; }

        public Route()
            :base()
        {
            this.Deliveries = new HashSet<Delivery>();
            this.RoutesClients = new HashSet<RouteClient>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }

        public void UpdateData(Route updated)
        {
            throw new NotImplementedException();
        }
    }
}
