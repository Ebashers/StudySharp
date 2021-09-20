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
    public sealed class AddTagCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
    }

    public sealed class AddTagCommandHandler : IRequestHandler<AddTagCommand, OperationResult>
    {
        private readonly StudySharpDbContext _studySharpDbContext;

        public AddTagCommandHandler(StudySharpDbContext studySharpDbContext)
        {
            _studySharpDbContext = studySharpDbContext;
        }

        public async Task<OperationResult> Handle(AddTagCommand request, CancellationToken cancellationToken)
        {
            if (await _studySharpDbContext.Tags.AnyAsync(_ => _.Name.ToLower().Equals(request.Name.ToLower()), cancellationToken))
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityAlreadyExists, nameof(Tag), nameof(Tag.Name), request.Name));
            }

            await _studySharpDbContext.Tags.AddAsync(new Tag { Name = request.Name });
            await _studySharpDbContext.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}
