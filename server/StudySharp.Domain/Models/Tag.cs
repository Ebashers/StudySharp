using System.Collections.Generic;

namespace StudySharp.Domain.Models
{
    public sealed class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
    }
}
