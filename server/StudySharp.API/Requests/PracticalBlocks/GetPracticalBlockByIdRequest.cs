using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.PracticalBlocks
{
    public class GetPracticalBlockByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty(Name = "courseId", SupportsGet = true)]
        public int CourseId { get; set; }
    }
}