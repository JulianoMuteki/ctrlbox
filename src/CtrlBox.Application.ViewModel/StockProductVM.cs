using System;

namespace CtrlBox.Application.ViewModel
{
    public class StockProductVM : ViewModelBase
    {
        public Guid StockID { get; set; }
        public Guid ProductID { get; set; }

        public StockVM Stock { get; set; }
        public ProductVM Product { get; set; }

        public int Amount { get; set; }
    }
}
