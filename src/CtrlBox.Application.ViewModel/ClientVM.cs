using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class ClientVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }

        public bool SaleIsFinished { get; set; }
        public SaleVM SaleVM { get; set; }

        public Guid AddressID { get; set; }
        public virtual AddressVM Address { get; set; }

        public ICollection<ClientProductValueVM> CustomersProductsValues { get; set; }
        public ICollection<SaleVM> Sales { get; set; }
        public ICollection<RouteClientVM> RoutesClients { get; set; }
        public ICollection<TrackingClientVM> TrackingsClients { get; set; }
        public ICollection<ClientOptionTypeVM> ClientsOptionsTypes { get; set; }
        public ICollection<RouteVM> Routes { get; set; }
        public ICollection<DeliveryDetailVM> DeliveriesDetails { get; set; }

        public ICollection<string> OptionsTypesID { get; set; }

        public ClientVM()
        {
            this.OptionsTypesID = new List<string>();
            this.DeliveriesDetails = new HashSet<DeliveryDetailVM>();
            this.Routes = new HashSet<RouteVM>();
            this.ClientsOptionsTypes = new HashSet<ClientOptionTypeVM>();
            this.TrackingsClients = new HashSet<TrackingClientVM>();
            this.RoutesClients = new HashSet<RouteClientVM>();
            this.Sales = new HashSet<SaleVM>();
            this.CustomersProductsValues = new HashSet<ClientProductValueVM>();
        }

        public void SetClientsCategoriesID()
        {
            if (this.ClientsOptionsTypes.Count > 0)
                this.OptionsTypesID = ClientsOptionsTypes.Select(x => x.OptiontTypeID.ToString()).ToArray();
        }
    }
}
