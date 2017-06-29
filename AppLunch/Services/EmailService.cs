using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SendGrid;
using System.Configuration;
using SendGrid.Helpers.Mail;

namespace AppLunch.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var client = new SendGridClient(ConfigurationManager.AppSettings["sendGridKey"]);
            var from = new EmailAddress(ConfigurationManager.AppSettings["fromAddress"]);
            var to = new EmailAddress(message.Destination);
            var email = MailHelper.CreateSingleEmail(from, to, message.Subject, message.Body, message.Body);

            await client.SendEmailAsync(email);
        }
    }
}