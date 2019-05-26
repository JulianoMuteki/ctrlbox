using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public interface ICustomEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
