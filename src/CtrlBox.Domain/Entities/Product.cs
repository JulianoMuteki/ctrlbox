using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        public string UnitMeasure { get; set; }

        public ICollection<ClientProductValue> CustomersProductsValues { get; set; }
        public ICollection<DeliveryProduct> DeliveriesProducts { get; set; }
        public ICollection<StockProduct> StocksProducts { get; set; }
        public ICollection<SaleProduct> SalesProducts { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid();
            this.DateModified = DateTime.Now;
            this.CreationDate = DateTime.Now;
            this.DeliveriesProducts = new HashSet<DeliveryProduct>();
            this.StocksProducts = new HashSet<StockProduct>();
            this.SalesProducts = new HashSet<SaleProduct>();
            this.CustomersProductsValues = new HashSet<ClientProductValue>();
        }

        public void UpdateData(Product update)
        {
            this.Name = update.Name;
            this.Description = update.Description;
            this.Weight = update.Weight;
            this.DateModified = DateTime.Now;
        }
    }
}
