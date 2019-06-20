using Microsoft.AspNetCore.Identity;
using System;

namespace CtrlBox.Infra.Context.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
