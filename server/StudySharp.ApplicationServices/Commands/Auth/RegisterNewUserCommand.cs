using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudySharp.ApplicationServices.ValidationRules.Auth;
using StudySharp.Domain.Constants;
using StudySharp.Domain.General;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.Commands.Auth
{
    public sealed class RegisterNewUserCommand : IRequest<OperationResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
    {
        public RegisterNewUserCommandValidator(IRegisterNewUserValidationRules rules)
        {
            RuleFor(_ => _.Email)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(RegisterNewUserCommand.Email)))
                .MustAsync(rules.UserNameIsUnique)
                .WithMessage(_ => string.Format(ErrorConstants.EntityAlreadyExists, "User", nameof(RegisterNewUserCommand.Email), _.Email));

            RuleFor(_ => _.Password)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(RegisterNewUserCommand.Password)));

            RuleFor(_ => _.ConfirmPassword)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, "Confirm Password"))
                .Must((registerNewUserCommand, confirmPassword) => registerNewUserCommand.Password.Trim().Equals(confirmPassword.Trim()))
                .WithMessage(ErrorConstants.PasswordsDoNotMatch);
        }
    }

    public sealed class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, OperationResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterNewUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OperationResult> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                return OperationResult.Fail(result.Errors.Select(_ => _.Description).ToList());
            }

            return OperationResult.Ok();
        }
    }
}
