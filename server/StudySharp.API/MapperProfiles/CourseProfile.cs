using AutoMapper;
using StudySharp.API.Requests.Courses;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.Queries;

namespace StudySharp.API.MapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<AddCourseRequest, AddCourseCommand>();
            CreateMap<RemoveCourseByIdRequest, RemoveCourseByIdCommand>();
            CreateMap<GetCourseByIdRequest, GetCourseByIdQuery>();
            CreateMap<GetCoursesRequest, GetCoursesQuery>();
            CreateMap<GetCoursesByTeacherIdRequest, GetCoursesByTeacherIdQuery>();
        }
    }
}
