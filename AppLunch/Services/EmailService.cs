using AppLunch.Interfaces;
using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using System.Threading.Tasks;

namespace AppLunch.Services
{
    public class EmailService : IIdentityMessageService, IEmailService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var client = new SendGridClient(ConfigurationManager.AppSettings["sendGridKey"]);
            var from = new EmailAddress(ConfigurationManager.AppSettings["fromAddress"]);
            var to = new EmailAddress(message.Destination);
            var email = MailHelper.CreateSingleEmail(from, to, message.Subject, message.Body, message.Body);

            await client.SendEmailAsync(email);
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            var client = new SendGridClient(ConfigurationManager.AppSettings["sendGridKey"]);
            var from = new EmailAddress(ConfigurationManager.AppSettings["fromAddress"]);
            var email = MailHelper.CreateSingleEmail(from, new EmailAddress(to), subject, body, body);

            await client.SendEmailAsync(email);
        }
    }
}