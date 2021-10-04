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
    public sealed class GetTheoryBlockByIdQuery : IRequest<OperationResult<TheoryBlock>>
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
    }

    public sealed class GetTheoryBlockByIdQueryHandler : IRequestHandler<GetTheoryBlockByIdQuery, OperationResult<TheoryBlock>>
    {
        private readonly StudySharpDbContext _context;

        public GetTheoryBlockByIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<TheoryBlock>> Handle(GetTheoryBlockByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.AnyAsync(_ => _.Id == request.CourseId, cancellationToken);
            if (!course)
            {
                return OperationResult.Fail<TheoryBlock>(string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), request.CourseId));
            }

            var theoryBlock = await _context.TheoryBlocks.FirstOrDefaultAsync(_ => _.Id == request.Id && _.CourseId == request.CourseId, cancellationToken);
            if (theoryBlock == null)
            {
                return OperationResult.Fail<TheoryBlock>(string.Format(ErrorConstants.EntityNotFound, nameof(TheoryBlock), nameof(TheoryBlock.Id), request.Id));
            }

            return OperationResult.Ok(theoryBlock);
        }
    }
}