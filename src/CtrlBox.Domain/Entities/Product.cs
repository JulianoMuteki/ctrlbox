using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }

        public ICollection<CustomerProductValue> CustomersProductsValues { get; set; }

        public Product()
        {
            this.CustomersProductsValues = new HashSet<CustomerProductValue>();
        }
    }
}
