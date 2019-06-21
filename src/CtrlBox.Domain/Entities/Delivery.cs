using CtrlBox.Domain.Common;
using CtrlBox.Domain.Identity;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Delivery : EntityBase
    {
        public Guid RouteID { get; set; }
        public Guid UserID { get; set; }

        public bool IsFinalized { get; set; }
        public DateTime DtStart { get; set; }
        public DateTime? DtEnd { get; set; }
        public string CreatedBy { get; set; }
        public string FinalizedBy { get; set; }

        public Route Route { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<DeliveryProduct> DeliveriesProducts { get; set; }
        public ICollection<Sale> Sales { get; set; }

        public Delivery()
        {
            Init();

            this.Expenses = new HashSet<Expense>();
            this.DeliveriesProducts = new HashSet<DeliveryProduct>();
            this.Sales = new HashSet<Sale>();
        }

        /// <summary>
        /// Inicialize property and ID - GUID
        /// </summary>
        public void Init()
        {
            this.InitBase();
            this.IsFinalized = false;
            this.Id = Guid.NewGuid();
            this.DtStart = DateTime.Now;
            this.CreatedBy = "Juliano";
            this.FinalizedBy = "Juliano";
        }

        public void FinalizeDelivery()
        {
            this.IsFinalized = true;
            this.DateModified = DateTime.Now;
            this.FinalizedBy = "Juliano";
        }
    }
}
