using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TNMarketplace.Service
{
    public class AuthMessageSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}
