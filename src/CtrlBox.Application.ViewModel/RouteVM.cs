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

        public virtual ICollection<DeliveryVM> Deliveries { get; set; }
        public virtual ICollection<RouteClientVM> RoutesClients { get; set; }

        public RouteVM()
        {
            this.Deliveries = new HashSet<DeliveryVM>();
            this.RoutesClients = new HashSet<RouteClientVM>();
        }

    }
}
