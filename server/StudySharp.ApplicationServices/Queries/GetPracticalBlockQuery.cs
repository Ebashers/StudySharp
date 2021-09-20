using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Queries
{
    public sealed class GetPracticalBlockQuery : IRequest<OperationResult<List<PracticalBlock>>>
    {
    }

    public sealed class GetCoursesQueryHandler : IRequestHandler<GetPracticalBlockQuery, OperationResult<List<PracticalBlock>>>
    {
        private readonly StudySharpDbContext _context;

        public GetCoursesQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<List<PracticalBlock>>> Handle(GetPracticalBlockQuery request, CancellationToken cancellationToken)
        {
            var practicalBlock = await _context.PracticalBlocks.ToListAsync(cancellationToken);
            return OperationResult.Ok(practicalBlock);
        }
    }
}