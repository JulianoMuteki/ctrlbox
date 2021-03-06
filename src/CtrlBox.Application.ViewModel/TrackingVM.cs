﻿using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class TrackingVM : ViewModelBase
    {
        public Guid? ProductItemID { get; set; }
        public ProductItemVM ProductItem { get; set; }

        public Guid? BoxID { get; set; }
        public BoxVM Box { get; set; }

        public Guid ClientID { get; set; }
        public bool IsLastTrack { get; set; }

        public Guid TrackingTypeID { get; set; }
        public TrackingTypeVM TrackingType { get; set; }

        public ICollection<TrackingClientVM> TrackingsClients { get; set; }
    }
}
