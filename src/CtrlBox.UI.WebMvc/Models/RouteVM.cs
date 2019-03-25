using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
{
    public class RouteVM: ViewModelBase
    {
        public string Name { get; set; }
        public int KmDistance { get; set; }
        public string Truck { get; set; }
        public bool HasOpenDelivery { get; set; }

    }
}