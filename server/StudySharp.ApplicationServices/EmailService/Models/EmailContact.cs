namespace StudySharp.ApplicationServices.EmailService.Models
{
    public class EmailContact
    {
        public EmailContact()
        {
        }

        public EmailContact(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
