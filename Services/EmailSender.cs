using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Pai.Services
{
    public class EmailSender : IEmailSender
    {
        public AuthMessageSenderOptions Options => new AuthMessageSenderOptions();

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mailClient = new SmtpClient(Options.MailServer, Options.MailPort)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true
            };
            mailClient.EnableSsl = true;
            mailClient.Credentials = new NetworkCredential(Options.Sender, Options.Password);

            mailClient.Send(Options.Sender, email, subject, message);
            return Task.CompletedTask;
        }
    }
}
