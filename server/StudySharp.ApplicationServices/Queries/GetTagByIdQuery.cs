using MediatR;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;

namespace StudySharp.ApplicationServices.Queries
{
    public sealed class GetTagByIdQuery : IRequest<OperationResult<Tag>>
    {
        public int Id { get; set; }
    }
}
