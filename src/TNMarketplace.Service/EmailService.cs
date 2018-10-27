using Postal;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TNMarketplace.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly DataCacheService cacheService;
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //Task.Factory.StartNew(() =>
            //{
            //    try
            //    {
            //        //skip email if there is no settings
            //        if (string.IsNullOrEmpty(cacheService.Settings.SmtpHost) && string.IsNullOrEmpty(cacheService.Settings.SmtpPassword))
            //            return;
            //        var service = ;

            //        var message = Postal.EmailService().;

            //        using (var smtpClient = new SmtpClient())
            //        {
            //            smtpClient.UseDefaultCredentials = false;

            //            // set credential if there is one
            //            if (!string.IsNullOrEmpty(cacheService.Settings.SmtpUserName) && !string.IsNullOrEmpty(cacheService.Settings.SmtpPassword))
            //            {
            //                var credential = new NetworkCredential
            //                {
            //                    UserName = cacheService.Settings.SmtpUserName,
            //                    Password = cacheService.Settings.SmtpPassword
            //                };
            //                smtpClient.Credentials = credential;
            //            }
            //            smtpClient.Host = cacheService.Settings.SmtpHost;
            //            smtpClient.EnableSsl = cacheService.Settings.SmtpSSL;

            //            if (cacheService.Settings.SmtpPort.HasValue)
            //                smtpClient.Port = cacheService.Settings.SmtpPort.Value;

            //            //moving CSS to inline style attributes, to gain maximum E-mail client compatibility.
            //            if (preMailer)
            //                message.Body = PreMailer.Net.PreMailer.MoveCssInline(message.Body).Html;

            //            smtpClient.Send(message);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        //http://stackoverflow.com/questions/7441062/how-to-use-elmah-to-manually-log-errors
            //        //httpContext.Raise(ex);
            //    }
            //});
            return Task.FromResult(0);
        }
    }

}
