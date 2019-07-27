using System;

namespace CtrlBox.Application.ViewModel
{
    public class BoxTrackingClientVM
    {
        public Guid BoxTrackingID { get; set; }
        public BoxTrackingVM BoxTracking { get; set; }

        public Guid ClientID { get; set; }
        public ClientVM Client { get; set; }
    }
}
