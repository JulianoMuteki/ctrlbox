using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModels
{
    public class RouteVM : ViewModelBase
    {
        public string Name { get; set; }
        public int KmDistance { get; set; }
        public string Truck { get; set; }
        public bool HasOpenDelivery { get; set; }
    }
}
