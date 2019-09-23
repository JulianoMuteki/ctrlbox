using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class ClientOptionType : ValueObject<ClientOptionType>
    {
        public Guid ClientID { get; set; }
        public Client Client { get; set; }

        public Guid OptiontTypeID { get; set; }
        public OptiontType OptiontType { get; set; }

        private ClientOptionType()
        {
          
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }

        internal static ClientOptionType FactoryCreate(Guid clientID, Guid optiontTypeID)
        {
            return new ClientOptionType()
            {
                ClientID = clientID,
                OptiontTypeID = optiontTypeID
            };
        }
    }
}
