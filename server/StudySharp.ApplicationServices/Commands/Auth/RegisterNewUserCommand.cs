using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

    public sealed class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, OperationResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterNewUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OperationResult> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            // compare passwords?
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user != null)
            {
                return OperationResult.Fail(string.Format(ErrorConstants.EntityAlreadyExists, "User", nameof(request.Email), request.Email));
            }

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

            // redirect to confirm email and then login?
            return OperationResult.Ok();
        }
    }
}
