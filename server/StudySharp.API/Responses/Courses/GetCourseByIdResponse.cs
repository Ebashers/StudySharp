using System;
using System.Collections.Generic;
using StudySharp.Domain.Models;
namespace StudySharp.API.Responses.Courses
{
    public class GetCourseByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tag> Tags { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<TheoryBlock> TheoryBlocks { get; set; }
        public List<PracticalBlock> PracticalBlocks { get; set; }
        public DateTimeOffset DateCreated { get; private set; }
        public DateTimeOffset? DateModified { get; private set; }
    }
}
