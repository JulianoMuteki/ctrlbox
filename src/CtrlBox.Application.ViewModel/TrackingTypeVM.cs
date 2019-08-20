using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class TrackingTypeVM : ViewModelBase
    {
        public string TrackType { get; set; }
        public string Description { get; set; }

        public Guid? PictureID { get; set; }
        public PictureVM Picture { get; set; }

        public ICollection<TrackingVM> Trackings { get; set; }
    }
}
