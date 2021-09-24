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
    public sealed class AddTheoryBlockCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
    }

    public sealed class AddTheoryBlockCommandHandler : IRequestHandler<AddTheoryBlockCommand, OperationResult>
    {
        private readonly StudySharpDbContext _context;

        public AddTheoryBlockCommandHandler(StudySharpDbContext sharpDbContext)
        {
            _context = sharpDbContext;
        }

        public async Task<OperationResult> Handle(AddTheoryBlockCommand request, CancellationToken cancellationToken)
        {
            if (await _context.TheoryBlocks.AnyAsync(_ => Equals(_.Name.ToLower(), request.Name.ToLower()) && _.CourseId == request.CourseId, cancellationToken))
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityAlreadyExists, nameof(TheoryBlock), nameof(TheoryBlock.Name), request.Name));
            }

            await _context.TheoryBlocks.AddAsync(
                new TheoryBlock
            {
                Name = request.Name, CourseId = request.CourseId,
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}