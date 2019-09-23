using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class OptiontType : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public EClientType EClientType { get; set; }

        public ICollection<ClientOptionType> ClientsOptionsTypes { get; set; }

        private OptiontType()
        : base()
        {
            this.ClientsOptionsTypes = new HashSet<ClientOptionType>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }
    }
}
