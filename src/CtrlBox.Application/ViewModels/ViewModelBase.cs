using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtrlBox.Application.ViewModels
{
    public abstract class ViewModelBase
    {
        public Guid DT_RowId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateModified { get; set; }
    }
}
