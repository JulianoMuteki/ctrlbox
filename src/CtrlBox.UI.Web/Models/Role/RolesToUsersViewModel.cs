using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CtrlBox.UI.Web.Models.Role
{
    public class RolesToUsersViewModel
    {
        public IEnumerable<SelectListItem> AllUsers { get; set; }
        public IEnumerable<SelectListItem> AllRoles { get; set; }

        public string RoleSelected { get; set; }
        public string UserSelected { get; set; }

        public RolesToUsersViewModel()
        {
            this.AllRoles = new HashSet<SelectListItem>();
            this.AllUsers = new HashSet<SelectListItem>();

        }
    }
}
