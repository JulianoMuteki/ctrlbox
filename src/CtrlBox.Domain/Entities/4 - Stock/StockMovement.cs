using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Common;
using System;

namespace CtrlBox.Domain.Entities
{
    public class StockMovement : EntityBase
    {
        public Guid ClientSupplierID { get; private set; }
        public Guid StockID { get; private set; }

        public decimal UnitPrice { get; private set; }
        public decimal TotalValue { get; private set; }

        public int Amount { get; private set; }
        public EFlowStepStock FlowStepStock { get; private set; }

        public Client ClientSupplier { get; private set; }
        public Stock Stock { get; private set; }

        public StockMovement()
            : base()
        {

        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
                FlowStepStock = EFlowStepStock.Input;
            }
        }

        public void SetStock(Stock stock)
        {
            TotalValue = Amount * UnitPrice;
            this.StockID = stock.Id;

            if (this.FlowStepStock == EFlowStepStock.Input)
                stock.InputStock(Amount);
            else if(this.FlowStepStock == EFlowStepStock.Output)
                stock.OutputStock(Amount);
        }
    }
}
