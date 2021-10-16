using System.Collections.Generic;

namespace StudySharp.Domain.Models
{
    public sealed class Teacher
    {
        public int Id { get; set; }
        public List<Course> Courses { get; set; }
    }
}
