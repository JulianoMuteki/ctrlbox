using System;

namespace CtrlBox.Application.ViewModel
{
   public class StockMovementVM: ViewModelBase
    {
        public ClientVM ClientSupplier { get; set; }
        public StockVM Stock { get; set; }

        public Guid ClientSupplierID { get; set; }
        public Guid StockID { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TotalValue { get; set; }

        public int Amount { get; set; }
        public string FlowStepStock { get; set; }

        public Guid ProductID { get; set; }
    }
}
