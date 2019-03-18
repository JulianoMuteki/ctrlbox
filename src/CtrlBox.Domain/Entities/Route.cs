using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class Route : EntityBase
    {
        public string Name { get; set; }
        public int KmDistance { get; set; }
        public string Truck { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

        public Route()
        {
            this.Deliveries = new HashSet<Delivery>();
            this.Clients = new HashSet<Client>();
        }
    }
}
