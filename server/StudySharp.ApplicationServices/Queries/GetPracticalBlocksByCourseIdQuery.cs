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
    public sealed class GetPracticalBlocksByCourseIdQuery : IRequest<OperationResult<PracticalBlock>>
    {
        public int CourseId { get; set; }
    }

    public sealed class GetPracticalBlockByCourseIdQueryHandler : IRequestHandler<GetPracticalBlocksByCourseIdQuery, OperationResult<PracticalBlock>>
    {
        private readonly StudySharpDbContext _context;

        public GetPracticalBlockByCourseIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<PracticalBlock>> Handle(GetPracticalBlocksByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var courseExistent = await _context.Courses.AnyAsync(_ => _.Id == request.CourseId, cancellationToken);
            if (!courseExistent)
            {
                return OperationResult.Fail<PracticalBlock>(string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), request.CourseId));
            }

            var practicalBlock = await _context.PracticalBlocks.FindAsync(request.CourseId);
            if (practicalBlock == null)
            {
                return OperationResult.Fail<PracticalBlock>(string.Format(ErrorConstants.EntityNotFound, nameof(Course.PracticalBlocks), nameof(PracticalBlock.CourseId), request.CourseId));
            }

            return OperationResult.Ok(practicalBlock);
        }
    }
}