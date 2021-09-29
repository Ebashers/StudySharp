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
    public sealed class RemoveTagByIdCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }

    public class RemoveTagByIdCommandValidator : AbstractValidator<RemoveTagByIdCommand>
    {
        public RemoveTagByIdCommandValidator(ITagRules rules)
        {
            RuleFor(_ => _.Id)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(Tag.Id)))
                .MustAsync((_, token) => rules.IsTagIdExistAsync(_))
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, nameof(Tag), nameof(Tag.Id), _.Id));
        }
    }

    public sealed class RemoveTagCommandHandler : IRequestHandler<RemoveTagByIdCommand, OperationResult>
    {
        private readonly StudySharpDbContext _studySharpDbContext;

        public RemoveTagCommandHandler(StudySharpDbContext studySharpDbContext)
        {
            _studySharpDbContext = studySharpDbContext;
        }

        public async Task<OperationResult> Handle(RemoveTagByIdCommand request, CancellationToken cancellationToken)
        {
            var tag = await _studySharpDbContext.Tags.FindAsync(request.Id, cancellationToken);
            _studySharpDbContext.Tags.Remove(tag);
            await _studySharpDbContext.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}