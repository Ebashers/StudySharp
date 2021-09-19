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
    public sealed class RemoveCourseCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
    }

    public sealed class RemoveCourseCommandHandler : IRequestHandler<RemoveCourseCommand, OperationResult>
    {
        private readonly StudySharpDbContext _context;
        public RemoveCourseCommandHandler(StudySharpDbContext sharpDbContext)
        {
            _context = sharpDbContext;
        }

        public async Task<OperationResult> Handle(RemoveCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => string.Equals(c.Name, request.Name), cancellationToken: cancellationToken);
            if (course == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityAlreadyExists, nameof(Course), nameof(Course.Name), request.Name));
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}