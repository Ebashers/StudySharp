using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.Courses
{
    public class GetCoursesByTeacherIdRequest
    {
        [BindProperty(Name = "teacherId", SupportsGet = true)]
        public int TeacherId { get; set; }
    }
}
