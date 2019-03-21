using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public int QuantityBoxes { get; set; }
        public float BalanceDue { get; set; }

        public ICollection<ClientProductValue> CustomersProductsValues { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public virtual ICollection<RouteClient> RoutesClients { get; set; }

        public Client()
        {
            this.RoutesClients = new HashSet<RouteClient>();
            this.Sales = new HashSet<Sale>();         
            this.CustomersProductsValues = new HashSet<ClientProductValue>();
        }
    }
}
