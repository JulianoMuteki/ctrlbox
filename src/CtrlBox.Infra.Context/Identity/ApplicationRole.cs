using Microsoft.AspNetCore.Identity;
using System;

namespace CtrlBox.Infra.Context.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {

        }
    }
}
