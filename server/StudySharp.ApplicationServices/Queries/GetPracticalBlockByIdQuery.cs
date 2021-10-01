using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Queries
{
    public sealed class GetPracticalBlockByIdQuery : IRequest<OperationResult<PracticalBlock>>
    {
        public int Id { get; set; }
    }

    public sealed class GetPracticalBlockByIdQueryHandler : IRequestHandler<GetPracticalBlockByIdQuery, OperationResult<PracticalBlock>>
    {
        private readonly StudySharpDbContext _context;

        public GetPracticalBlockByIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<PracticalBlock>> Handle(GetPracticalBlockByIdQuery request, CancellationToken cancellationToken)
        {
            var practicalBlock = await _context.PracticalBlocks.FindAsync(request.Id, cancellationToken);
            if (practicalBlock == null)
            {
                return OperationResult.Fail<PracticalBlock>(string.Format(ErrorConstants.EntityNotFound, nameof(PracticalBlock), nameof(PracticalBlock.Id), request.Id));
            }

            return OperationResult.Ok(practicalBlock);
        }
    }
}