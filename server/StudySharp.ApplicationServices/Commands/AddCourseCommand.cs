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

namespace StudySharp.ApplicationServices.Commands
{
    public sealed class AddCourseCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
        public int TeacherId { get; set; }
    }

    public class AddCourseCommandValidator : AbstractValidator<AddCourseCommand>
    {
        public AddCourseCommandValidator(ICourseRules rules)
        {
            RuleFor(_ => new { _.Name, _.TeacherId })
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(Course.Name)))
                .MustAsync((courseTeacherAndName, token) => rules.IsNameUniqueAsync(courseTeacherAndName.Name, courseTeacherAndName.TeacherId, token))
                .WithMessage(_ => string.Format(ErrorConstants.EntityAlreadyExists, nameof(Course), nameof(Course.Name), _.Name));

            RuleFor(_ => _.TeacherId)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(Course.TeacherId)))
                .MustAsync(rules.IsTeacherIdExistAsync)
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, nameof(Teacher), nameof(Teacher.Id), _.TeacherId));
        }
    }

    public sealed class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, OperationResult>
    {
        private readonly StudySharpDbContext _context;

        public AddCourseCommandHandler(StudySharpDbContext sharpDbContext)
        {
            _context = sharpDbContext;
        }

        public async Task<OperationResult> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Courses.AnyAsync(_ => _.Name.ToLower().Equals(request.Name.ToLower()) && _.TeacherId == request.TeacherId))
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityAlreadyExists, nameof(Course), nameof(Course.Name), request.Name));
            }

            await _context.Courses.AddAsync(
                new Course
                {
                    Name = request.Name,
                    TeacherId = request.TeacherId,
                }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}
