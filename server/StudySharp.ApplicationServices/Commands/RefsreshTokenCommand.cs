using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudySharp.ApplicationServices.JwtService;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands
{
    public sealed class RefreshTokenCommand : IRequest<OperationResult<LoginResult>>
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public sealed class RefsreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, OperationResult<LoginResult>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtAuthManager _jwtAuthManager;

        public RefsreshTokenCommandHandler(IJwtAuthManager jwtAuthManager, UserManager<ApplicationUser> userManager)
        {
            _jwtAuthManager = jwtAuthManager;
            _userManager = userManager;
        }

        public async Task<OperationResult<LoginResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    return OperationResult.Fail<LoginResult>(string.Format(ErrorConstants.EntityNotFound, "User", "Email", request.UserName));
                }

                if (string.IsNullOrEmpty(request.RefreshToken))
                {
                    return OperationResult.Fail<LoginResult>(ErrorConstants.InvalidCredentials);
                }

                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, request.AccessToken, DateTime.Now);
                return OperationResult.Ok<LoginResult>(new LoginResult
                {
                    UserName = user.UserName,
                    Role = "Test",
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString,
                });
            }
            catch (SecurityTokenException exception)
            {
                return OperationResult.Fail<LoginResult>(exception.Message); // return 401 so that the client side can redirect the user to login page
            }
        }
    }
}
