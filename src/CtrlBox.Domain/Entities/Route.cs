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

        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<RouteClient> RoutesClients { get; set; }

        public Route()
        {
            this.Deliveries = new HashSet<Delivery>();
            this.RoutesClients = new HashSet<RouteClient>();
        }
    }
}
