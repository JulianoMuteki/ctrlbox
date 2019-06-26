using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class PaymentMethodVM : ViewModelBase
    {
        public string MethodName { get; set; }
        public string Descrition { get; set; }

        public ICollection<PaymentVM> PaymentsVMs { get; set; }
    }
}
