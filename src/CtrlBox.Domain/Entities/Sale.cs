using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class Sale : EntityBase
    {
        public Guid ClientID { get; set; }
        public Guid DeliveryID { get; set; }

        public decimal ReceivedValue { get; set; }
        public decimal ForwardValue { get; set; }
        public int TotalReturnedBoxes { get; set; }
        public bool IsFinished { get; set; }

        public Client   Client { get; set; }
        public Delivery Delivery { get; set; }

        public ICollection<Check> Checks { get; set; }
        public ICollection<DeliveryProduct> DeliveriesProducts { get; set; }
        public ICollection<SaleProduct> SalesProducts { get; set; }

        public void Init()
        {
            this.Id = Guid.NewGuid();
            this.DateModified = DateTime.Now;
            this.CreationDate = DateTime.Now;
            this.IsFinished = true;

        }

        public Sale()
        {
            this.SalesProducts = new HashSet<SaleProduct>();
            this.Checks = new HashSet<Check>();
            this.DeliveriesProducts = new HashSet<DeliveryProduct>();

            Init();
        }
    }
}
