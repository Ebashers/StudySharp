using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.TheoryBlocks
{
    public class AddTheoryBlockRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [BindProperty(Name = "courseId", SupportsGet = true)]
        public int CourseId { get; set; }
    }
}