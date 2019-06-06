using System;

namespace CtrlBox.Application.ViewModel
{
    public abstract class ViewModelBase
    {
        public string DT_RowId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateModified { get; set; }
        public Guid ID { get { return new Guid(this.DT_RowId); } }
    }
}