using StudySharp.Domain.Constants;

namespace StudySharp.ApplicationServices.EmailService.Models
{
    public class Email
    {
        public Email(EmailTemplate template)
        {
            Subject = template.Subject;
            Body = WrapAsHtmlDocument(template.Subject, template.Layout, template.Body);
        }

        public Email(string subject, string body, string layout = EmailLayouts.Default)
        {
            Subject = subject;
            Body = WrapAsHtmlDocument(subject, layout, body);
        }

        public string Body { get; }
        public string Subject { get; }

        private static string WrapAsHtmlDocument(string subject, string layout, string body)
        {
            return string.Format(layout, subject, body);
        }
    }
}
