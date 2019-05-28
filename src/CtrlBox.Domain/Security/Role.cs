using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CtrlBox.Domain.Security
{
    public enum Role
    {

        [Display(GroupName = "Manager", Name = "Admin", Description = "System admin")]
        Admin,

        [Display(GroupName = "Manager", Name = "Manager", Description = "View all system")]
        Manager,

        [Display(GroupName = "Staff", Name = "Staff", Description = "Operation")]
        Staff,

        [Display(GroupName = "Client", Name = "Client", Description = "Just client")]
        Client
    }
}
