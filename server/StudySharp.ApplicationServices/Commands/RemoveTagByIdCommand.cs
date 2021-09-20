using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands
{
    public sealed class RemoveTagByIdCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }

    public sealed class RemoveTagCommandHandler : IRequestHandler<RemoveTagByIdCommand, OperationResult>
    {
        private readonly StudySharpDbContext _studySharpDbContext;

        public async Task<OperationResult> Handle(RemoveTagByIdCommand request, CancellationToken cancellationToken)
        {
            var tag = await _studySharpDbContext.Tags.FindAsync(request.Id, cancellationToken);
            if (tag == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(Tag), nameof(Tag.Id), request.Id));
            }

            _studySharpDbContext.Tags.Remove(tag);
            await _studySharpDbContext.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}