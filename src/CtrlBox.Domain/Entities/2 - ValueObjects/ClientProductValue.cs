using CtrlBox.Domain.Common;
using CtrlBox.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class ClientProductValue : ValueObject<ClientProductValue>
    {
        public Guid ClientID { get; set; }
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public Client Client { get; set; }

        public float Price { get; set; }

        private ClientProductValue()
        {
            base.ComponentValidator.Validate(this, new ClientProductValueValidator());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}
