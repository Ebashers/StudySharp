using System.Collections.Generic;

namespace StudySharp.Domain.Models
{
    public sealed class Teacher
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Course> Courses { get; set; }
    }
}
