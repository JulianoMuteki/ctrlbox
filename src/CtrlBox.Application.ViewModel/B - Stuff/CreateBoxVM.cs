﻿using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class CreateBoxVM
    {
        public string Description { get; set; }
        public Guid BoxTypeID { get; set; }
        public IList<string> TagsBarcodes { get; set; }
    }
}
