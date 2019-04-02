using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class ProductVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }

        public ICollection<ClientProductValueVM> CustomersProductsValues { get; set; }
        public ICollection<DeliveryProductVM> DeliveriesProducts { get; set; }
        public ICollection<StockProductVM> StocksProducts { get; set; }
        public ICollection<SaleProductVM> SalesProducts { get; set; }

        public ProductVM()
        {
            this.DeliveriesProducts = new HashSet<DeliveryProductVM>();
            this.StocksProducts = new HashSet<StockProductVM>();
            this.SalesProducts = new HashSet<SaleProductVM>();
            this.CustomersProductsValues = new HashSet<ClientProductValueVM>();
        }
    }
}
