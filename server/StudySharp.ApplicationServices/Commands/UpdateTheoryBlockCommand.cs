using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands
{
    public sealed class UpdateTheoryBlockCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
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
            var theoryBlock = await _context.TheoryBlocks.FindAsync(request.Id, cancellationToken);

            if (theoryBlock == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, nameof(TheoryBlock), nameof(TheoryBlock.Name), request.Id));
            }

            theoryBlock.Name = request.Name;
            theoryBlock.Description = request.Description;

            _context.TheoryBlocks.Update(theoryBlock);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}