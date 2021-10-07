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
    public sealed class GetTheoryBlocksByCourseIdQuery : IRequest<OperationResult<TheoryBlock>>
    {
        public int CourseId { get; set; }
    }

    public sealed class GetTheoryBlockByCourseIdQueryHandler : IRequestHandler<GetTheoryBlocksByCourseIdQuery, OperationResult<TheoryBlock>>
    {
        private readonly StudySharpDbContext _context;

        public GetTheoryBlockByCourseIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<TheoryBlock>> Handle(GetTheoryBlocksByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var courseExistent = await _context.Courses.AnyAsync(_ => _.Id == request.CourseId, cancellationToken);
            if (!courseExistent)
            {
                return OperationResult.Fail<TheoryBlock>(string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), request.CourseId));
            }

            var theoryBlock = await _context.TheoryBlocks.FindAsync(request.CourseId);
            if (theoryBlock == null)
            {
                return OperationResult.Fail<TheoryBlock>(string.Format(ErrorConstants.EntityNotFound, nameof(Course.TheoryBlocks), nameof(TheoryBlock.CourseId), request.CourseId));
            }

            return OperationResult.Ok(theoryBlock);
        }
    }
}