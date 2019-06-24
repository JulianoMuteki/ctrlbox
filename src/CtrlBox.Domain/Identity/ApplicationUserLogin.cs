using Microsoft.AspNetCore.Identity;
using System;

namespace CtrlBox.Domain.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
