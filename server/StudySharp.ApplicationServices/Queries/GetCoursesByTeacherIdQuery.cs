using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.Constants;
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
            var isTeacherExistent = await _context.Teachers.AnyAsync(_ => _.Id == request.TeacherId, cancellationToken);
            if (!isTeacherExistent)
            {
                return OperationResult.Fail<List<Course>>(string.Format(ErrorConstants.EntityNotFound, nameof(Teacher), nameof(Teacher.Id), request.TeacherId));
            }

            var courses = await _context.Courses.Where(_ => _.TeacherId == request.TeacherId).ToListAsync(cancellationToken);
            return OperationResult.Ok(courses);
        }
    }
}
