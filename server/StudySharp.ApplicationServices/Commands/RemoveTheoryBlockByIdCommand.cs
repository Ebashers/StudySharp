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
    public sealed class RemoveTheoryBlockByIdCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
    }

    public sealed class RemoveTheoryBlockByIdCommandHandler : IRequestHandler<RemoveTheoryBlockByIdCommand, OperationResult>
    {
        private readonly StudySharpDbContext _context;

        public RemoveTheoryBlockByIdCommandHandler(StudySharpDbContext sharpDbContext)
        {
            _context = sharpDbContext;
        }

        public async Task<OperationResult> Handle(RemoveTheoryBlockByIdCommand request, CancellationToken cancellationToken)
        {
            var theoryBlock = await _context.TheoryBlocks.FirstOrDefaultAsync(_ => _.Id == request.Id && _.CourseId == request.CourseId);

            if (theoryBlock == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(TheoryBlock), nameof(TheoryBlock.Id), nameof(TheoryBlock.CourseId), request.Id, request.CourseId));
            }

            _context.TheoryBlocks.Remove(theoryBlock);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}
