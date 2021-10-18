namespace StudySharp.ApplicationServices.Infrastructure.EmailService
{
    public class EmailServiceSettings
    {
        public string Name { get; set; }
        public string From { get; set; }
        public string Smtp { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
