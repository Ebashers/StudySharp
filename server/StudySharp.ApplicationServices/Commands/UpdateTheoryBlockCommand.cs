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
    public sealed class UpdateTheoryBlockCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class UpdateTheoryBlockCommandHandler : IRequestHandler<UpdateTheoryBlockCommand, OperationResult>
    {
        private readonly StudySharpDbContext _context;

        public UpdateTheoryBlockCommandHandler(StudySharpDbContext sharpDbContext)
        {
            _context = sharpDbContext;
        }

        public async Task<OperationResult> Handle(UpdateTheoryBlockCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Courses.AnyAsync(_ => _.Id != request.CourseId, cancellationToken))
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), request.CourseId));
            }

            var theoryBlock = await _context.TheoryBlocks.FirstOrDefaultAsync(_ => _.Id == request.Id && _.CourseId == request.CourseId, cancellationToken);

            if (theoryBlock == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(TheoryBlock), nameof(TheoryBlock.Name), nameof(TheoryBlock.CourseId), request.Id, request.CourseId));
            }

            theoryBlock.Name = request.Name;
            theoryBlock.Description = request.Description;

            _context.TheoryBlocks.Update(theoryBlock);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}