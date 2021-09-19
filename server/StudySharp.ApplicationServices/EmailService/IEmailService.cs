using System.Threading.Tasks;

namespace StudySharp.ApplicationServices.EmailService
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}
