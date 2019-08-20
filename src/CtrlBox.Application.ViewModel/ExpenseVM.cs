using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class ExpenseVM: ViewModelBase
    {
        public Guid DeliveryID { get; set; }

        public double Value { get; set; }

        public string Description { get; set; }

        public virtual OrderVM Delivery { get; set; }
    }
}
