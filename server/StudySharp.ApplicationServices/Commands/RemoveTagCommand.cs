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
    public sealed class RemoveTagCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
    }

    public sealed class RemoveTagCommandHandler : IRequestHandler<RemoveTagCommand, OperationResult>
    {
        private readonly StudySharpDbContext _studySharpDbContext;

        public async Task<OperationResult> Handle(RemoveTagCommand request, CancellationToken cancellationToken)
        {
            if (await _studySharpDbContext.Tags.AnyAsync(_ => _.Name.ToLower() != request.Name.ToLower()))
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(Tag), nameof(Tag.Name), request.Name));
            }

            _studySharpDbContext.Tags.Remove(new Tag { Name = request.Name });
            await _studySharpDbContext.SaveChangesAsync();
            return OperationResult.Ok();
        }
    }
}