using MediatR;
using StudySharp.Domain.General;

namespace StudySharp.ApplicationServices.Commands
{
    public sealed class LogoutCommand : IRequest<OperationResult>
    {
    }
}
