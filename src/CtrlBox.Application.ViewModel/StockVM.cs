using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class StockVM: ViewModelBase
    {
        public int AmountBoxes { get; set; }

        public virtual ICollection<StockProductVM> StocksProducts { get; set; }

        public StockVM()
        {
            this.StocksProducts = new HashSet<StockProductVM>();
        }
    }
}
