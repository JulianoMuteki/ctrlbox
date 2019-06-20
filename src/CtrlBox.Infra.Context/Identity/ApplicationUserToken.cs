using Microsoft.AspNetCore.Identity;
using System;

namespace CtrlBox.Infra.Context.Identity
{
    public class ApplicationUserToken : IdentityUserToken<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
