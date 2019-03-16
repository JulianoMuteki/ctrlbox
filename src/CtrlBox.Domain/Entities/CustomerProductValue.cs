using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class CustomerProductValue : ValueObject<CustomerProductValue>
    {
        public Guid ClientID { get; set; }
        public Guid ProductID { get; set; }
        public float Price { get; set; }

        public Product Product { get; set; }

        public Client Client { get; set; }

        protected override bool EqualsCore(CustomerProductValue other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
