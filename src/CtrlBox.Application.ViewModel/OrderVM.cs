using System;
using System.Text;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class OrderVM : ViewModelBase
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

        public ICollection<ExpenseVM> Expenses { get; set; }
        public ICollection<DeliveryDetailVM> DeliveriesDetails { get; set; }
        public ICollection<OrderBoxVM> OrdersBoxes { get; set; }
        public ICollection<SaleVM> Sales { get; set; }
        public ICollection<BoxProductItemVM> BoxesProductItems { get; set; }

        public ICollection<BoxTypeVM> BoxesTypes { get; set; }
        public OrderVM()
        {
            this.BoxesTypes = new HashSet<BoxTypeVM>();
            this.BoxesProductItems = new HashSet<BoxProductItemVM>();
            this.Expenses = new HashSet<ExpenseVM>();
            this.DeliveriesDetails = new HashSet<DeliveryDetailVM>();
            this.OrdersBoxes = new HashSet<OrderBoxVM>();
            this.Sales = new HashSet<SaleVM>();
        }
    }
}
