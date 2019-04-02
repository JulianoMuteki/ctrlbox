using System;

namespace CtrlBox.Application.ViewModel
{
    public class StockProductVM
    {
        public Guid StockID { get; set; }
        public Guid ProductID { get; set; }

        public virtual StockVM Stock { get; set; }
        public virtual ProductVM Product { get; set; }

        public int Amount { get; set; }

    }
}
