using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class CustomEmailSender : ICustomEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
