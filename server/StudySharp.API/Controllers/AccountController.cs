using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudySharp.ApplicationServices.Commands;
using StudySharp.Domain.General;

namespace StudySharp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<OperationResult> Register([FromBody] RegisterNewUserCommand registerNewUserCommand)
        {
            return await _mediator.Send<OperationResult>(registerNewUserCommand);
        }

        [HttpPost("login")]
        public async Task<OperationResult<LoginResult>> Login([FromBody] LoginCommand loginCommand)
        {
            return await _mediator.Send<OperationResult<LoginResult>>(loginCommand);
        }

        [HttpPost("logout")]
        public async Task<OperationResult> Logout([FromBody] LogoutCommand logoutCommand)
        {
            return await _mediator.Send<OperationResult>(logoutCommand);
        }
    }
}
