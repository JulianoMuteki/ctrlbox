using CtrlBox.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface ISecurityApplicationService
    {
        IList<ApplicationUser> GetAllUsers();
    }
}
