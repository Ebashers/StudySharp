using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Queries
{
    public sealed class GetTheoryBlockByIdQuery : IRequest<OperationResult<TheoryBlock>>
    {
        public int Id { get; set; }
    }

    public sealed class GetTheoryBlockByIdQueryHandler : IRequestHandler<GetTheoryBlockByIdQuery, OperationResult<TheoryBlock>>
    {
        private readonly StudySharpDbContext _context;

        public GetTheoryBlockByIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<TheoryBlock>> Handle(
            GetTheoryBlockByIdQuery request,
            CancellationToken cancellationToken)
        {
            var theoryBlock = await _context.TheoryBlocks.FindAsync(request.Id, cancellationToken);
            if (theoryBlock == null)
            {
                return OperationResult.Fail<TheoryBlock>(string.Format(ErrorConstants.EntityNotFound, nameof(TheoryBlock), nameof(TheoryBlock.Id), request.Id));
            }

            return OperationResult.Ok(theoryBlock);
        }
    }
}