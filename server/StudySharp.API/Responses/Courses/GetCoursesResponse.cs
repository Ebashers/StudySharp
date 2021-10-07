using System.Collections.Generic;
using StudySharp.Domain.Models;

namespace StudySharp.API.Responses.Courses
{
    public class GetCoursesResponse
    {
        public List<Course> Courses { get; set; }
    }
}
