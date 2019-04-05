using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel.Custom
{
    class ExecuteDeliveryVM
    {
        public IList<DeliveryProductVM> DeliverysProducts { get; set; }
        public IList<ClientVM> Clients { get; set; }
        public IList<ExpenseVM> Expenses { get; set; }

    }
}
