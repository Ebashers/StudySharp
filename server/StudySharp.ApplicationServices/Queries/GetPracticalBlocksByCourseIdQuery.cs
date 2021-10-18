using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.Domain.ValidationRules;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Queries
{
    public sealed class GetPracticalBlocksByCourseIdQuery : IRequest<OperationResult<List<PracticalBlock>>>
    {
        public int CourseId { get; set; }
    }

    public class GetPracticalBlocksByCourseIdQueryValidator : AbstractValidator<GetPracticalBlocksByCourseIdQuery>
    {
        public GetPracticalBlocksByCourseIdQueryValidator(IPracticalBlockRules rules)
        {
            RuleFor(_ => _.CourseId)
                .MustAsync((_, token) => rules.IsCourseIdExistAsync(_, token))
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(PracticalBlock.CourseId), _.CourseId));
        }
    }

    public sealed class GetPracticalBlockByCourseIdQueryHandler : IRequestHandler<GetPracticalBlocksByCourseIdQuery, OperationResult<List<PracticalBlock>>>
    {
        private readonly StudySharpDbContext _context;

        public GetPracticalBlockByCourseIdQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<List<PracticalBlock>>> Handle(GetPracticalBlocksByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var practicalBlocks = await _context.PracticalBlocks.Where(_ => _.CourseId == request.CourseId).ToListAsync(cancellationToken);
            return OperationResult.Ok(practicalBlocks);
        }
    }
}