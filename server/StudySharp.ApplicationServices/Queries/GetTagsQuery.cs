using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Queries
{
    public sealed class GetTagsQuery : IRequest<OperationResult<List<Tag>>>
    {
    }

    public sealed class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, OperationResult<List<Tag>>>
    {
        private readonly StudySharpDbContext _context;

        public GetTagsQueryHandler(StudySharpDbContext studySharpDbContext)
        {
            _context = studySharpDbContext;
        }

        public async Task<OperationResult<List<Tag>>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _context.Tags.ToListAsync(cancellationToken);
            return OperationResult.Ok(tags);
        }
    }
}