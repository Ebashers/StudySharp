using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using StudySharp.ApplicationServices.ValidationRules;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.Domain.ValidationRules;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands
{
    public sealed class RemoveCourseByIdCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }

    public class RemoveCourseByIdCommandValidator : AbstractValidator<RemoveCourseByIdCommand>
    {
        public RemoveCourseByIdCommandValidator(ICourseRules rules)
        {
            RuleFor(_ => _.Id)
                .MustAsync((_, token) => rules.IsCourseIdExistAsync(_, token))
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), _.Id));
        }
    }

    public sealed class RemoveCourseByIdCommandHandler : IRequestHandler<RemoveCourseByIdCommand, OperationResult>
    {
        private readonly StudySharpDbContext _context;

        public RemoveCourseByIdCommandHandler(StudySharpDbContext sharpDbContext)
        {
            _context = sharpDbContext;
        }

        public async Task<OperationResult> Handle(RemoveCourseByIdCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FindAsync(request.Id, cancellationToken);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}