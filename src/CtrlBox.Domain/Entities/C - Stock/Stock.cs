using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Stock: EntityBase
    {
        public Guid ClientID { get; private set; }
        public Guid ProductID { get; private set; }

        public int Minimum { get; private set; }
        public int TotalStock { get; private set; }
        public decimal DefaultPrice { get; private set; }

        public Client Client { get; private set; }
        public Product Product { get; private set; }

        public ICollection<StockMovement> StocksMovements { get; private set; }

        public Stock()
            :base()
        {
            this.StocksMovements = new HashSet<StockMovement>();
        }

        public static Stock FactoryCreate(Guid clientID, Guid productID)
        {
            Stock stock = new Stock();
            stock.Minimum = 10;
            stock.TotalStock = 0;
            stock.DefaultPrice = 0;
            stock.ClientID = clientID;
            stock.ProductID = productID;

            return stock;
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }

        internal void OutputStock(int amount)
        {
            this.TotalStock -= amount;
            this.DateModified = DateTime.Now;
        }

        internal void InputStock(int amount)
        {
            this.TotalStock += amount;
            this.DateModified = DateTime.Now;
        }

        public void AddMovementInput(int totalProductItems)
        {
            InputStock(totalProductItems);
            this.StocksMovements.Add(StockMovement.FactoryCreate(this.ClientID, this.Id, totalProductItems, CrossCutting.Enums.EFlowStepStock.Input));
        }

        public void SubtractMovementInput(int totalProductItems)
        {
            OutputStock(totalProductItems);
            this.StocksMovements.Add(StockMovement.FactoryCreate(this.ClientID, this.Id, totalProductItems, CrossCutting.Enums.EFlowStepStock.Output));
        }
    }
}
