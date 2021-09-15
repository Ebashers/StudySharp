using MediatR;
using StudySharp.Domain.General;

namespace StudySharp.ApplicationServices.Commands
{
    public sealed class AddTagCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
    }
}
