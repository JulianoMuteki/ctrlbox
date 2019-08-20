using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class RouteVM: ViewModelBase
    {
        public string ClientesIDs { get; set; }

        public string Name { get; set; }
        public int KmDistance { get; set; }
        public string Truck { get; set; }
        public bool HasOpenDelivery { get; set; }

        public Guid ClientOriginID { get; set; }
        public ClientVM ClientOrigin { get; set; }

        public virtual ICollection<OrderVM> Deliveries { get; set; }
        public virtual ICollection<RouteClientVM> RoutesClients { get; set; }

        public RouteVM()
        {
            this.Deliveries = new HashSet<OrderVM>();
            this.RoutesClients = new HashSet<RouteClientVM>();
        }

    }
}
