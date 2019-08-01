using CtrlBox.Domain.Common;
using CtrlBox.Domain.Validations;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }

        public Guid AddressID { get; set; }
        public virtual Address Address { get; set; }

        public ICollection<ClientProductValue> CustomersProductsValues { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<RouteClient> RoutesClients { get; set; }
        public ICollection<TrackingClient> TrackingsClients { get; set; }
        public ICollection<ClientOptionType> ClientsOptionsTypes { get; set; }
        public ICollection<Route> Routes { get; set; }
        public ICollection<DeliveryBox> DeliveriesBoxes { get; set; }
        
        public Client()
            :base()
        {
            this.DeliveriesBoxes = new HashSet<DeliveryBox>();
            this.Routes = new HashSet<Route>();
            this.ClientsOptionsTypes = new HashSet<ClientOptionType>();
            this.TrackingsClients = new HashSet<TrackingClient>();
            this.RoutesClients = new HashSet<RouteClient>();
            this.Sales = new HashSet<Sale>();         
            this.CustomersProductsValues = new HashSet<ClientProductValue>();
        }

        public void SetOptionsTypes(ICollection<string> optionsTypesID)
        {
            if (optionsTypesID.Count > 0)
            {
                foreach (var optionTypeID in optionsTypesID)
                {
                    ClientOptionType clientOptionType = new ClientOptionType()
                    {
                        ClientID = this.Id,
                        OptiontTypeID = new Guid(optionTypeID)
                    };

                    this.ClientsOptionsTypes.Add(clientOptionType);
                }
            }
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
                base.ComponentValidator.Validate(this, new ClientValidator());
                this.Contact = "Juliano";
            }
        }
    }
}
