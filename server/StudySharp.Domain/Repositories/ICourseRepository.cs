using System.Collections.Generic;
using System.Threading.Tasks;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;

namespace StudySharp.Domain.Repositories
{
    public interface ICourseRepository : IRepository
    {
        Task<OperationResult<Course>> CreateCourseAsync(Course course);
        Task<OperationResult<Course>> GetCourseByIdAsync(int id);
        Task<OperationResult<List<Course>>> GetCoursesAsync();
        Task<OperationResult<List<Course>>> GetCoursesByUserIdAsync(int userId);
        Task<OperationResult<Course>> UpdateCourseAsync(Course course);
        Task<OperationResult<Course>> RemoveCourseByIdAsync(int id);
    }
}