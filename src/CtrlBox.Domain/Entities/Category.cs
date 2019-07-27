using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Category: EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ClientCategory> ClientsCategories { get; set; }

        public Category()
        : base()
        {
            this.ClientsCategories = new HashSet<ClientCategory>();
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
