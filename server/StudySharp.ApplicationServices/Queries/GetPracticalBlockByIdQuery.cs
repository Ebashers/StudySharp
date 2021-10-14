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
    public sealed class GetPracticalBlockByIdQuery : IRequest<OperationResult<PracticalBlock>>
    {
        public int Id { get; set; }
    }

    public class GetPracticalBlockByIdQueryValidator : AbstractValidator<GetPracticalBlockByIdQuery>
    {
        public GetPracticalBlockByIdQueryValidator(IPracticalBlockRules rules)
        {
            RuleFor(_ => _.Id)
                .MustAsync((_, token) => rules.IsPracticalBlockIdExistAsync(_, token))
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, nameof(PracticalBlock), nameof(PracticalBlock.Id), _.Id));
        }
    }

    public sealed class GetPracticalBlockByIdQueryHandler : IRequestHandler<GetPracticalBlockByIdQuery, OperationResult<PracticalBlock>>
    {
        private readonly StudySharpDbContext _context;

        public GetPracticalBlockByIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<PracticalBlock>> Handle(GetPracticalBlockByIdQuery request, CancellationToken cancellationToken)
        {
            var practicalBlock = await _context.PracticalBlocks.FindAsync(request.Id, cancellationToken);
            return OperationResult.Ok(practicalBlock);
        }
    }
}