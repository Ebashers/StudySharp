using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using StudySharp.ApplicationServices.EmailService.Models;
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

        public async Task SendEmailAsync(Email email, EmailContact contact)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_settings.Name, _settings.From));
            emailMessage.To.Add(new MailboxAddress(contact.Name, contact.Email));
            emailMessage.Subject = email.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = email.Body,
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Smtp, _settings.Port, false);
            await client.AuthenticateAsync(_settings.UserName, _settings.Password);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
