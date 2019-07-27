using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class ClientCategory : ValueObject<ClientProductValue>
    {
        public Guid ClientID { get; set; }
        public Client Client { get; set; }

        public Guid CategoryID { get; set; }
        public Category Category { get; set; }

        public ClientCategory()
        {

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}
