using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.ValidationRules;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.ValidationRules
{
    public class CourseRules : ICourseRules
    {
        private readonly StudySharpDbContext _context;

        public CourseRules(StudySharpDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsNameUniqueAsync(string name, int teacherId, CancellationToken cancellationToken)
        {
            return !await _context.Courses.AnyAsync(_ => _.Name == name && _.TeacherId == teacherId, cancellationToken);
        }

        public async Task<bool> IsCourseIdExistAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Courses.AnyAsync(_ => _.Id == id, cancellationToken);
        }

        public async Task<bool> IsTeacherIdExistAsync(int teacherId, CancellationToken cancellationToken)
        {
            return await _context.Teachers.AnyAsync(_ => _.Id == teacherId, cancellationToken);
        }
    }
}