using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.Domain.ValidationRules;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Queries
{
    public sealed class GetTagByIdQuery : IRequest<OperationResult<Tag>>
    {
        public int Id { get; set; }
    }

    public class GetTagByIdQueryValidator : AbstractValidator<GetTagByIdQuery>
    {
        public GetTagByIdQueryValidator(ITagRules rules)
        {
            RuleFor(_ => _.Id)
                .MustAsync((_, token) => rules.IsTagIdExistAsync(_, token))
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, nameof(Tag), nameof(Tag.Id), _.Id));
        }
    }

    public sealed class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, OperationResult<Tag>>
    {
        private readonly StudySharpDbContext _studySharpDbContext;

        public GetTagByIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _studySharpDbContext = studySharpDbContext;
        }

        public async Task<OperationResult<Tag>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _studySharpDbContext.Tags.FindAsync(request.Id, cancellationToken);
            return OperationResult.Ok(tag);
        }
    }
}
