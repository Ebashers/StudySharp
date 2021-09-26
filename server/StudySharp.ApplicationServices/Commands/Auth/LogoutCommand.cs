using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudySharp.ApplicationServices.JwtAuthService;
using StudySharp.ApplicationServices.ValidationRules.Auth;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands.Auth
{
    public sealed class LogoutCommand : IRequest<OperationResult>
    {
        public string UserName { get; set; }
    }

    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator(ILogoutRules rules)
        {
            RuleFor(_ => _.UserName)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(ApplicationUser.UserName)))
                .MustAsync(rules.UserIsRegistered)
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, "User", nameof(ApplicationUser.Email), _.UserName));
        }
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

            _jwtService.RemoveRefreshTokenByUserName(user.UserName);

            return OperationResult.Ok();
        }
    }
}
