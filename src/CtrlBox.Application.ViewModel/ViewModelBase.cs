using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Application.ViewModel
{
    public abstract class ViewModelBase
    {
        public string DT_RowId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateModified { get; set; }

        public IReadOnlyCollection<KeyValuePair<string, string>> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();
        private List<KeyValuePair<string, string>> _notifications;

        public ViewModelBase()
        {
            _notifications = new List<KeyValuePair<string, string>>();
        }
        public void SetNotifications(List<KeyValuePair<string, string>> notifications)
        {
            _notifications = notifications;
        }
    }
}