using CtrlBox.Domain.Common;
using CtrlBox.Domain.Validations;
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
        public ICollection<BoxTrackingClient> TracesClients { get; set; }

        public Client()
            :base()
        {
            this.TracesClients = new HashSet<BoxTrackingClient>();
            this.RoutesClients = new HashSet<RouteClient>();
            this.Sales = new HashSet<Sale>();         
            this.CustomersProductsValues = new HashSet<ClientProductValue>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
                base.ComponentValidator.Validate(this, new ClientValidator());               
            }
        }
    }
}
