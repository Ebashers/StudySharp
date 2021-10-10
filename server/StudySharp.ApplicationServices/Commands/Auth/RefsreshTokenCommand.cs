using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudySharp.ApplicationServices.JwtAuthService;
using StudySharp.ApplicationServices.JwtAuthService.ResultModels;
using StudySharp.ApplicationServices.ValidationRules.Auth;
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

    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator(IRefreshTokenRules rules)
        {
            RuleFor(_ => _.UserName)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(ApplicationUser.Email)))
                .MustAsync(rules.UserIsRegistered)
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, "User", nameof(ApplicationUser.Email), _.UserName));

            RuleFor(_ => _.AccessToken)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(RefreshTokenCommand.AccessToken)));

            RuleFor(_ => _.RefreshToken)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(RefreshTokenCommand.RefreshToken)));
        }
    }

    public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, OperationResult<RefreshTokenResult>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public RefreshTokenCommandHandler(IJwtService jwtService, UserManager<ApplicationUser> userManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<OperationResult<RefreshTokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                var userRoles = await _userManager.GetRolesAsync(user);
                var roleClaims = userRoles.Select(_ => new Claim(ClaimsIdentity.DefaultRoleClaimType, _));
                var userClaims = new List<Claim>(roleClaims)
                {
                    new (ClaimsIdentity.DefaultNameClaimType, user.UserName),
                };

                var jwtResult = _jwtService.Refresh(request.RefreshToken, request.AccessToken, userClaims, DateTime.UtcNow);
                return OperationResult.Ok(new RefreshTokenResult
                {
                    UserName = user.UserName,
                    Roles = userRoles.ToList(),
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
