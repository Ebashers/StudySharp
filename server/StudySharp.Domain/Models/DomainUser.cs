namespace StudySharp.Domain.Models
{
    public sealed class DomainUser
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
