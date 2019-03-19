using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtrlBox.Application.ViewModels
{
    public class ClientVM: ViewModelBase
    {
        public string Name { get; set; }
        public int QuantityBoxes { get; set; }
        public float BalanceDue { get; set; }
    }
}
