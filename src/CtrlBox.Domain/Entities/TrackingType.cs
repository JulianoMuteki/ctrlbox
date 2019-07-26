using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class TrackingType : EntityBase
    {
        public TypeTrace TypeTrace { get; set; }
        public string Description { get; set; }

        public Guid? PictureID { get; set; }
        public Picture Picture { get; set; }

        public ICollection<BoxTracking> BoxesTrackings { get; set; }

        public TrackingType()
            : base()
        {
            this.BoxesTrackings = new HashSet<BoxTracking>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }
    }

    public enum TypeTrace
    {
        Place = 1,
        State = 2
    }
}
