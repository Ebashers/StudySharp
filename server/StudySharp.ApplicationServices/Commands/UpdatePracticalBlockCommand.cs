using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Digests;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.Domain.ValidationRules;
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

    public class UpdatePracticalBlockCommandValidator : AbstractValidator<UpdatePracticalBlockCommand>
    {
        public UpdatePracticalBlockCommandValidator(IPracticalBlockRules rules)
        {
            RuleFor(_ => _.CourseId)
                .MustAsync((_, token) => rules.IsCourseIdExistAsync(_, token))
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, nameof(Course), nameof(Course.Id), _.CourseId));
            RuleFor(_ => _.Id)
                .MustAsync((_, token) => rules.IsPracticalBlockIdExistAsync(_, token))
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, nameof(PracticalBlock), nameof(PracticalBlock.Id), _.Id));
        }
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
            var practicalBlock = await _context.PracticalBlocks.FindAsync(request.Id, cancellationToken);
            practicalBlock.Name = request.Name;
            practicalBlock.Description = request.Description;
            _context.PracticalBlocks.Update(practicalBlock);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Ok();
        }
    }
}