using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands
{
    public sealed class UpdatePracticalBlockCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public sealed class UpdatePracticalBlockCommandHandler : IRequestHandler<UpdatePracticalBlockCommand, OperationResult>
    {
        private readonly StudySharpDbContext _studySharpDbContext;

        public async Task<OperationResult> Handle(UpdatePracticalBlockCommand request, CancellationToken cancellationToken)
        {
            var practicalBlock = await _studySharpDbContext.PracticalBlocks.FindAsync(request.Id, cancellationToken);
            if (practicalBlock == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(PracticalBlock), nameof(PracticalBlock.Id), request.Id));
            }

            practicalBlock.Name = request.Name;
            _studySharpDbContext.PracticalBlocks.Update(practicalBlock);
            await _studySharpDbContext.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}