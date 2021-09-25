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
    public sealed class AddCourseCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
        public int TeacherId { get; set; }
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
            if (await _context.Courses.AnyAsync(_ => Equals(_.Name.ToLower(), request.Name.ToLower()) && _.TeacherId == request.TeacherId, cancellationToken))
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityAlreadyExists, nameof(Course), nameof(Course.Name), request.Name));
            }

            await _context.Courses.AddAsync(
                new Course
            {
                    Name = request.Name, TeacherId = request.TeacherId,
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}