using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtrlBox.Application.ViewModels
{
    public class ClientVM: ViewModelBase
    {
        public string Name { get; set; }
        public string QuantityBoxes { get; set; }
        public string BalanceDue { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }

        public bool IsDelivery { get; set; }
    }
}
