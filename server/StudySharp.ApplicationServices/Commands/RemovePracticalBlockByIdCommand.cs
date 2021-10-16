using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.Domain.ValidationRules;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands
{
    public sealed class RemovePracticalBlockByIdCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }

    public class RemovePracticalBlockByIdCommandValidator : AbstractValidator<RemovePracticalBlockByIdCommand>
    {
        public RemovePracticalBlockByIdCommandValidator(IPracticalBlockRules rules)
        {
            RuleFor(_ => _.Id)
                .MustAsync((_, token) => rules.IsPracticalBlockIdExistAsync(_, token))
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, nameof(PracticalBlock), nameof(PracticalBlock.Id), _.Id));
        }
    }

    public sealed class RemovePracticalBlockByIdCommandHandler : IRequestHandler<RemovePracticalBlockByIdCommand, OperationResult>
    {
        private readonly StudySharpDbContext _studySharpDbContext;

        public async Task<OperationResult> Handle(RemovePracticalBlockByIdCommand request, CancellationToken cancellationToken)
        {
            var practicalBlock = await _studySharpDbContext.PracticalBlocks.FindAsync(request.Id, cancellationToken);
            _studySharpDbContext.PracticalBlocks.Remove(practicalBlock);
            await _studySharpDbContext.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}