using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.CrossCutting
{
   public enum BoxStatus
    {
        Empty = 0,
        Full = 1,
        IsClosed = 2,
        ReadyDelivery = 3,
        LoadRoute = 4,
        Delivered = 5,
        LoadRouteBack = 6
    }
}
