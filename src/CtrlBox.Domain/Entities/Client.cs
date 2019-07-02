using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public int QuantityBoxes { get; set; }
        public float BalanceDue { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }

        public Guid AddressID { get; set; }
        public virtual Address Address { get; set; }

        public ICollection<ClientProductValue> CustomersProductsValues { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<RouteClient> RoutesClients { get; set; }

        public Client()
        {
            this.Id = Guid.NewGuid();
            this.DateModified = DateTime.Now;
            this.CreationDate = DateTime.Now;

            this.RoutesClients = new HashSet<RouteClient>();
            this.Sales = new HashSet<Sale>();         
            this.CustomersProductsValues = new HashSet<ClientProductValue>();
        }
    }
}
