using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class StockVM: ViewModelBase
    {
        public ClientVM Client { get; set; }
        public ProductVM Product { get; set; }

        public Guid ClientID { get; set; }
        public Guid ProductID { get; set; }
        public int Minimum { get; set; }
        public int TotalStock { get; set; }
        public decimal DefaultPrice { get; set; }

    }
}
