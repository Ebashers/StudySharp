namespace StudySharp.ApplicationServices.EmailService.Models
{
    public class EmailTemplate
    {
        public string Subject { get; set; }
        public string Layout { get; set; }
        public string Body { get; set; }

        public Email Build(params string[] bodyParams)
        {
            return new Email(Subject, string.Format(Body, bodyParams), Layout);
        }
    }
}
