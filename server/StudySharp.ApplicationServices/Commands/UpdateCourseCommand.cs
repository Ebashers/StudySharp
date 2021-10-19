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
    public class UpdateCourseCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
    }

    public partial class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator(ICourseRules rules)
        {
            RuleFor(_ => _.Id)
                .MustAsync(rules.IsCourseIdExistAsync)
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), _.Id));

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

    public sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, OperationResult>
    {
        private readonly StudySharpDbContext _context;

        public UpdateCourseCommandHandler(StudySharpDbContext sharpDbContext)
        {
            _context = sharpDbContext;
        }

        public async Task<OperationResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken);
            course.Name = request.Name;
            course.TeacherId = request.TeacherId;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}