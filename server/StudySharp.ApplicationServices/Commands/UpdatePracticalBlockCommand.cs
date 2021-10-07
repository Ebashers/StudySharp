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
    public sealed class UpdatePracticalBlockCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class UpdatePracticalBlockCommandHandler : IRequestHandler<UpdatePracticalBlockCommand, OperationResult>
    {
        private readonly StudySharpDbContext _context;

        public UpdatePracticalBlockCommandHandler(StudySharpDbContext sharpDbContext)
        {
            _context = sharpDbContext;
        }

        public async Task<OperationResult> Handle(UpdatePracticalBlockCommand request, CancellationToken cancellationToken)
        {
            var courseExistent = await _context.Courses.AnyAsync(_ => _.Id == request.CourseId, cancellationToken);
            if (!courseExistent)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), request.CourseId));
            }

            var practicalBlock = await _context.PracticalBlocks.FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken);
            if (practicalBlock == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(PracticalBlock), nameof(PracticalBlock.Id), request.Id));
            }

            practicalBlock.Name = request.Name;
            practicalBlock.Description = request.Description;
            _context.PracticalBlocks.Update(practicalBlock);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}