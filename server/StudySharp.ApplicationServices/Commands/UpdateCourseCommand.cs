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
    public class UpdateCourseCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
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
            if (course == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), request.Id));
            }

            var teacherExistent = await _context.Teachers.AnyAsync(_ => _.Id == request.TeacherId, cancellationToken);
            if (!teacherExistent)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(Teacher), nameof(Teacher.Id), request.TeacherId));
            }

            course.Name = request.Name;
            course.TeacherId = request.TeacherId;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}