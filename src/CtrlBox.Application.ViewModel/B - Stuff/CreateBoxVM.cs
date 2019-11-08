using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class CreateBoxVM
    {
        public Guid ClientID { get; set; }
        public Guid ProductID { get; set; }

        public string Description { get; set; }
        public Guid BoxTypeID { get; set; }
        public IList<string> TagsBarcodes { get; set; }

        public bool HasMovementStock { get; set; }

        public bool HasAutoCompleteItems { get; set; }
        public int TotalItemsinBox { get; set; }

        public Guid ClientSupplierID { get; set; }
    }
}
