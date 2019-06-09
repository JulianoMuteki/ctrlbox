using System.ComponentModel.DataAnnotations;

namespace CtrlBox.Domain.Security
{
    public enum RoleAuthorize
    {

        [Display(GroupName = "Manager", Name = "Admin", Description = "System admin")]
        Admin,

        [Display(GroupName = "Manager", Name = "Manager", Description = "View all system")]
        Manager,

        [Display(GroupName = "Staff", Name = "Staff", Description = "Operation")]
        Staff,

        [Display(GroupName = "Client", Name = "Client", Description = "Just client")]
        Client,

        [Display(GroupName = "Delivery", Name = "Delivery", Description = "Delivery")]
        Delivery,
    }
}
