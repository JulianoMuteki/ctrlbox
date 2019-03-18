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

        public ICollection<CustomerProductValue> CustomersProductsValues { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Route> Routes { get; set; }

        public Client()
        {
            this.Routes = new HashSet<Route>();
            this.Sales = new HashSet<Sale>();         
            this.CustomersProductsValues = new HashSet<CustomerProductValue>();
        }
    }
}
