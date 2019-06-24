using System;
using System.Text;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class DeliveryVM : ViewModelBase
    {
        public Guid RouteID { get; set; }
        public Guid UserID { get; set; }

        public bool IsFinalized { get; set; }
        public DateTime DtStart { get; set; }
        public DateTime? DtEnd { get; set; }
        public string CreatedBy { get; set; }
        public string FinalizedBy { get; set; }

        public string RouteName { get; set; }
        public string UserName { get; set; }
        public RouteVM RouteVM { get; set; }

        public ICollection<DeliveryProductVM> DeliveriesProducts { get; set; }

        public DeliveryVM()
        {
            this.DeliveriesProducts = new HashSet<DeliveryProductVM>();
        }
    }
}
