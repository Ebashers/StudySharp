using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands
{
    public sealed class RemovePracticalBlockCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }

    public sealed class RemovePracticalBlockCommandHandler : IRequestHandler<RemovePracticalBlockCommand, OperationResult>
    {
        private readonly StudySharpDbContext _studySharpDbContext;

        public async Task<OperationResult> Handle(RemovePracticalBlockCommand request, CancellationToken cancellationToken)
        {
            var practicalBlock = await _studySharpDbContext.PracticalBlocks.FindAsync(request.Id, cancellationToken);
            if (practicalBlock == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(PracticalBlock), nameof(PracticalBlock.Id), request.Id));
            }

            _studySharpDbContext.PracticalBlocks.Remove(practicalBlock);
            await _studySharpDbContext.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}