using MediatR;
using StudySharp.Domain.General;

namespace StudySharp.ApplicationServices.Commands.Auth
{
    public sealed class LogoutCommand : IRequest<OperationResult>
    {
    }
}
