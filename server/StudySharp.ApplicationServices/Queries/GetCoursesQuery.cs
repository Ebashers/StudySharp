using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Queries
{
    public sealed class GetCoursesQuery : IRequest<OperationResult<List<Course>>>
    {
    }

    public sealed class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, OperationResult<List<Course>>>
    {
        private readonly StudySharpDbContext _context;

        public GetCoursesQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<List<Course>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _context.Courses.ToListAsync(cancellationToken);
            return OperationResult.Ok(courses);
        }
    }
}