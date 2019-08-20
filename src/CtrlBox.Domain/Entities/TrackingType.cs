using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class TrackingType : EntityBase
    {
        public ETrackType TrackType { get; set; }
        public string Description { get; set; }

        public Guid? PictureID { get; set; }
        public Picture Picture { get; set; }

        public ICollection<Tracking> Trackings { get; set; }

        public TrackingType()
            : base()
        {
            this.Trackings = new HashSet<Tracking>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }
    }
}
