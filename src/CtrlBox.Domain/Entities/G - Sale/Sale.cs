﻿using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class Sale : EntityBase
    {
        public Guid ClientID { get; set; }
        public Guid OrderID { get; set; }

        public decimal ReceivedValue { get; set; }
        public decimal ForwardValue { get; set; }
        public bool IsFinished { get; set; }

        public Client   Client { get; set; }
        public Order Order { get; set; }
        public Payment Payment { get; set; }

        public ICollection<SaleProduct> SalesProducts { get; set; }

        private Sale()
        {
            this.SalesProducts = new HashSet<SaleProduct>();

            Init();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                this.Id = Guid.NewGuid();
                this.DateModified = DateTime.Now;
                this.CreationDate = DateTime.Now;
                this.IsFinished = true;

                this.SalesProducts = this.SalesProducts.Select(x => { x.SaleID = this.Id; return x; }).ToList();
                this.Payment.SaleID = this.Id;
            }
        }

        public void CalculatePayment()
        {
            this.Payment.TotalValueSale = this.SalesProducts.Sum(x => x.TotalValue);
        }
    }
}
