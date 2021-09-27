using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.Courses
{
    public class GetCourseByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
    }
}
