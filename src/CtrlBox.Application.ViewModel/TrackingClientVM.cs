using System;

namespace CtrlBox.Application.ViewModel
{
    public class TrackingClientVM
    {
        public Guid BoxTrackingID { get; set; }
        public TrackingVM BoxTracking { get; set; }

        public Guid ClientID { get; set; }
        public ClientVM Client { get; set; }
    }
}
