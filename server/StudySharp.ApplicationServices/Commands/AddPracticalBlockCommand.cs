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
    public sealed class AddPracticalBlockCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
    }

    public sealed class AddPracticalBlockCommandHandler : IRequestHandler<AddPracticalBlockCommand, OperationResult>
    {
        private readonly StudySharpDbContext _studySharpDbContext;

        public AddPracticalBlockCommandHandler(StudySharpDbContext studySharpDbContext)
        {
            _studySharpDbContext = studySharpDbContext;
        }

        public async Task<OperationResult> Handle(AddPracticalBlockCommand request, CancellationToken cancellationToken)
        {
            if (await _studySharpDbContext.PracticalBlocks.AnyAsync(_ => _.Name.ToLower().Equals(request.Name.ToLower())))
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityAlreadyExists, nameof(PracticalBlock), nameof(PracticalBlock.Name), request.Name));
            }

            await _studySharpDbContext.PracticalBlocks.AddAsync(new PracticalBlock { Name = request.Name });
            await _studySharpDbContext.SaveChangesAsync();
            return OperationResult.Ok();
        }
    }
}