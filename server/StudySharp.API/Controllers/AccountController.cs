using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests;
using StudySharp.API.Responses;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.JwtService;
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
        public async Task<OperationResult> Register([FromBody] RegisterNewUserRequest registerNewUserRequest)
        {
            // AutoMapper is required ASAP!!!
            var registerNewUserCommand = new RegisterNewUserCommand
            {
                Email = registerNewUserRequest.Email,
                Password = registerNewUserRequest.Password,
                ConfirmPassword = registerNewUserRequest.ConfirmPassword,
            };

            return await _mediator.Send<OperationResult>(registerNewUserCommand);
        }

        [HttpPost("login")]
        public async Task<OperationResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            // AutoMapper is required ASAP!!!
            var loginCommand = new LoginCommand
            {
                Email = loginRequest.Email,
                Password = loginRequest.Password,
            };

            var operationResult = await _mediator.Send<OperationResult<LoginResult>>(loginCommand);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<LoginResponse>(operationResult.Errors);
            }

            var response = new LoginResponse
            {
                UserName = operationResult.Result.UserName,
                AccessToken = operationResult.Result.AccessToken,
                RefreshToken = operationResult.Result.RefreshToken,
                Role = operationResult.Result.Role,
            };

            return OperationResult.Ok<LoginResponse>(response);
        }

        [HttpPost("logout")]
        public async Task<OperationResult> Logout([FromBody] LogoutCommand logoutCommand)
        {
            return await _mediator.Send<OperationResult>(logoutCommand);
        }

        [HttpPost("refresh-token")]
        public async Task<OperationResult<LoginResult>> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var userName = User.Identity.Name;
            return await _mediator.Send<OperationResult<LoginResult>>(new RefreshTokenCommand { AccessToken = accessToken, RefreshToken = refreshTokenRequest.RefreshToken, UserName = userName });
        }
    }
}
