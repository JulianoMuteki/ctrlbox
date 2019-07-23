﻿using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class TraceTypeVM: ViewModelBase
    {
        public string TypeTrace { get; set; }
        public string Description { get; set; }

        public Guid? PictureID { get; set; }
        public PictureVM Picture { get; set; }

        public ICollection<TraceabilityVM> Traceabilities { get; set; }
    }
}