﻿using System.Linq;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class ProductVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        public string UnitMeasure { get; set; }

        public int StockTotal
        {
            get
            {
                return this.StocksProducts.Sum(x => x.Amount);
            }
        }

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
