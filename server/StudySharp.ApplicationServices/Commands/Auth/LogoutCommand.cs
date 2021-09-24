using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudySharp.ApplicationServices.JwtAuthService;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands.Auth
{
    public sealed class LogoutCommand : IRequest<OperationResult>
    {
        public string UserName { get; set; }
    }

    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, OperationResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public LogoutCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtAuthManager)
        {
            _userManager = userManager;
            _jwtService = jwtAuthManager;
        }

        public async Task<OperationResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityNotFound, "User", "Email", request.UserName));
            }

            _jwtService.RemoveRefreshTokenByUserName(user.UserName);

            return OperationResult.Ok();
        }
    }
}
