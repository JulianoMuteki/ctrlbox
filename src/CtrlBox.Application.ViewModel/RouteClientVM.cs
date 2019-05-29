using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class RouteClientVM
    {
        public Guid RouteID { get; set; }
        public Guid ClientID { get; set; }

        public RouteVM Route { get; set; }
        public ClientVM Client { get; set; }

    }
}
