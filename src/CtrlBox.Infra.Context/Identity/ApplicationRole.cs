using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CtrlBox.Infra.Context.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        public ApplicationRole()
        {

        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {

        }
    }
}
