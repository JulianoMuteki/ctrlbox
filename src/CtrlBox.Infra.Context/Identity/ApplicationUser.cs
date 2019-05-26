﻿using Microsoft.AspNetCore.Identity;
using System;

namespace CtrlBox.Infra.Context.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid ContactID { get; set; }
    }
}
