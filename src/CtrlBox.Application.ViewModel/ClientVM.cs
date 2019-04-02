using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
   public class ClientVM: ViewModelBase
    {
        public string Name { get; set; }
        public int QuantityBoxes { get; set; }
        public float BalanceDue { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }

        public bool IsDelivery { get; set; }

        public ICollection<ClientProductValueVM> CustomersProductsValues { get; set; }
        public ICollection<SaleVM> Sales { get; set; }
        public ICollection<RouteClientVM> RoutesClients { get; set; }

        public ClientVM()
        {
            this.RoutesClients = new HashSet<RouteClientVM>();
            this.Sales = new HashSet<SaleVM>();
            this.CustomersProductsValues = new HashSet<ClientProductValueVM>();
        }
    }
}
