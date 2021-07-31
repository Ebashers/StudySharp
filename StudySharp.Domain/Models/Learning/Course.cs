using System;
using System.Collections.Generic;
using StudySharp.Domain.Models.Users;

namespace StudySharp.Domain.Models.Learning
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tag> Tags { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? RedactedAt { get; set; }
        
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        
        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}