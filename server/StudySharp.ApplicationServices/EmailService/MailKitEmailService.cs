using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using StudySharp.ApplicationServices.Infrastructure.EmailService;

namespace StudySharp.ApplicationServices.EmailService
{
    public class MailKitEmailService : IEmailService
    {
        private readonly EmailServiceSettings _settings;
        public MailKitEmailService(IOptions<EmailServiceSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_settings.Name, _settings.From));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message,
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("ebashers.ss@gmail.com", "Gfhjkm1!");
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
