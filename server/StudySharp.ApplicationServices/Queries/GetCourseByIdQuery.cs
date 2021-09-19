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
    public sealed class GetCourseByIdQuery : IRequest<OperationResult<Course>>
    {
        public int Id { get; set; }
    }

    public sealed class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, OperationResult<Course>>
    {
        private readonly StudySharpDbContext _context;

        public GetCourseByIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<Course>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FindAsync(request.Id, cancellationToken);
            if (course == null)
            {
                return OperationResult.Fail<Course>(string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), request.Id));
            }

            return OperationResult.Ok(course);
        }
    }
}