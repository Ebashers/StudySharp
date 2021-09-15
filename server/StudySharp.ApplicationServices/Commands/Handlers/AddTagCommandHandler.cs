using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands.Handlers
{
    public sealed class AddTagCommandHandler : IRequestHandler<AddTagCommand, OperationResult>
    {
        private readonly StudySharpDbContext _studySharpDbContext;

        public AddTagCommandHandler(StudySharpDbContext studySharpDbContext)
        {
            _studySharpDbContext = studySharpDbContext;
        }

        public async Task<OperationResult> Handle(AddTagCommand request, CancellationToken cancellationToken)
        {
            if (await _studySharpDbContext.Tags.AnyAsync(_ => _.Name.ToLower().Equals(request.Name.ToLower())))
            {
                return OperationResult.Fail($"Tag with name [{request.Name}] already exists");
            }

            await _studySharpDbContext.Tags.AddAsync(new Tag { Name = request.Name });
            await _studySharpDbContext.SaveChangesAsync();
            return OperationResult.Ok();
        }
    }
}
