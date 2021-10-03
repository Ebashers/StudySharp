using System.Threading.Tasks;
using StudySharp.ApplicationServices.EmailService.Models;

namespace StudySharp.ApplicationServices.EmailService
{
    public interface IEmailService
    {
        public Task SendEmailAsync(Email email, EmailContact contact);
    }
}
