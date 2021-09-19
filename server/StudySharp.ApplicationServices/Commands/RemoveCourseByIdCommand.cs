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
    public sealed class RemoveCourseByIdCommand : IRequest<OperationResult>
    {
        public string Id { get; set; }
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
            var course = await _context.Courses.FirstOrDefaultAsync(c => Equals(c.Id, request.Id), cancellationToken);
            if (course == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Name), request.Id));
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}