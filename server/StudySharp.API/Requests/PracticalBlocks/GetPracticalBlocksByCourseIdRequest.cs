using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.PracticalBlocks
{
    public class GetPracticalBlocksByCourseIdRequest
    {
        [BindProperty(Name = "courseId", SupportsGet = true)]
        public int CourseId { get; set; }
    }
}