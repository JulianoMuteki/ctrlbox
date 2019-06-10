using System.ComponentModel.DataAnnotations;

namespace CtrlBox.Domain.Security
{
    public enum DeliveryClaim
    {
        [Display(GroupName = "Delivery", Name = "DeliveryClaim", Description = "Person can do a delivery")]
        ExecuteDelivery

    }
}
