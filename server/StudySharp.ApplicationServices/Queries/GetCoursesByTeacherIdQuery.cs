using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Queries
{
    public sealed class GetCoursesByTeacherIdQuery : IRequest<OperationResult<List<Course>>>
    {
        public int TeacherId { get; set; }
    }

    public sealed class GetCoursesByTeacherIdQueryHandler : IRequestHandler<GetCoursesByTeacherIdQuery, OperationResult<List<Course>>>
    {
        private readonly StudySharpDbContext _context;

        public GetCoursesByTeacherIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<List<Course>>> Handle(GetCoursesByTeacherIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await _context.Courses.Where(_ => _.TeacherId == request.TeacherId).ToListAsync();
            return OperationResult.Ok(courses);
        }
    }
}
