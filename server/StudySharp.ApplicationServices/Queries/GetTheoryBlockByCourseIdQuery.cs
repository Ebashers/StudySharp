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
    public sealed class GetTheoryBlockByCourseIdQuery : IRequest<OperationResult<TheoryBlock>>
    {
        public int CourseId { get; set; }
    }

    public sealed class GetTheoryBlockByCourseIdQueryHandler : IRequestHandler<GetTheoryBlockByCourseIdQuery, OperationResult<TheoryBlock>>
    {
        private readonly StudySharpDbContext _context;

        public GetTheoryBlockByCourseIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<TheoryBlock>> Handle(GetTheoryBlockByCourseIdQuery request, CancellationToken cancellationToken)
        {
            if (await _context.Courses.AnyAsync(_ => _.Id != request.CourseId, cancellationToken))
            {
                return OperationResult.Fail<TheoryBlock>(string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), request.CourseId));
            }

            var theoryBlock = await _context.TheoryBlocks.FindAsync(request.CourseId, cancellationToken);
            if (theoryBlock == null)
            {
                return OperationResult.Fail<TheoryBlock>(string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.TheoryBlocks), request.CourseId));
            }

            return OperationResult.Ok(theoryBlock);
        }
    }
}