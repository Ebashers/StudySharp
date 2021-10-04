using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.TheoryBlocks
{
    public class RemoveTheoryBlockByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty(Name = "courseId", SupportsGet = true)]
        public int CourseId { get; set; }
    }
}