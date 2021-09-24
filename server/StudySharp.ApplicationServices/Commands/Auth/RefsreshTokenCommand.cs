using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudySharp.ApplicationServices.JwtAuthService;
using StudySharp.ApplicationServices.JwtAuthService.ResultModels;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands.Auth
{
    public sealed class RefreshTokenCommand : IRequest<OperationResult<RefreshTokenResult>>
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public sealed class RefsreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, OperationResult<RefreshTokenResult>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public RefsreshTokenCommandHandler(IJwtService jwtAuthManager, UserManager<ApplicationUser> userManager)
        {
            _jwtService = jwtAuthManager;
            _userManager = userManager;
        }

        public async Task<OperationResult<RefreshTokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    return OperationResult.Fail<RefreshTokenResult>(string.Format(ErrorConstants.EntityNotFound, "User", "Email", request.UserName));
                }

                if (string.IsNullOrEmpty(request.RefreshToken))
                {
                    return OperationResult.Fail<RefreshTokenResult>(ErrorConstants.InvalidToken);
                }

                var jwtResult = _jwtService.Refresh(request.RefreshToken, request.AccessToken, DateTime.Now);
                return OperationResult.Ok(new RefreshTokenResult
                {
                    UserName = user.UserName,
                    Role = "Test",
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString,
                });
            }
            catch (SecurityTokenException exception)
            {
                return OperationResult.Fail<RefreshTokenResult>(exception.Message);
            }
        }
    }
}
