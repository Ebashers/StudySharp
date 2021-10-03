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
    public sealed class AddTagCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
        public int? Id { get; set; }
    }

    public class AddTagCommandValidator : AbstractValidator<AddTagCommand>
    {
        public AddTagCommandValidator(ITagRules rules)
        {
            RuleFor(_ => _.Name)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(Tag.Name)))
                .MustAsync((_, token) => rules.IsNameUniqueAsync(_))
                .WithMessage(_ => string.Format(ErrorConstants.EntityAlreadyExists, nameof(Tag), nameof(Tag.Name), _.Name));
        }
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
            await _studySharpDbContext.Tags.AddAsync(new Tag { Name = request.Name }, cancellationToken);
            await _studySharpDbContext.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}
