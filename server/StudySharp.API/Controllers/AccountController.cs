﻿using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests.Auth;
using StudySharp.API.Responses.Auth;
using StudySharp.ApplicationServices.Commands.Auth;
using StudySharp.ApplicationServices.JwtAuthService.ResultModels;
using StudySharp.Domain.General;

namespace StudySharp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<OperationResult> Register([FromBody] RegisterNewUserRequest registerNewUserRequest)
        {
            var registerNewUserCommand = _mapper.Map<RegisterNewUserCommand>(registerNewUserRequest);

            return await _mediator.Send<OperationResult>(registerNewUserCommand);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<OperationResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var loginCommand = _mapper.Map<LoginCommand>(loginRequest);

            var operationResult = await _mediator.Send<OperationResult<LoginResult>>(loginCommand);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<LoginResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<LoginResponse>(operationResult.Result);

            return OperationResult.Ok<LoginResponse>(response);
        }

        [HttpPost("logout")]
        public async Task<OperationResult> Logout([FromBody] LogoutCommand logoutCommand)
        {
            return await _mediator.Send<OperationResult>(logoutCommand);
        }

        [HttpPost("refresh-token")]
        public async Task<OperationResult<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var userName = User.Identity.Name;
            var refreshTokenCommand = new RefreshTokenCommand
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenRequest.RefreshToken,
                UserName = userName,
            };

            var operationResult = await _mediator.Send<OperationResult<RefreshTokenResult>>(refreshTokenCommand);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<RefreshTokenResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<RefreshTokenResponse>(operationResult.Result);

            return OperationResult.Ok<RefreshTokenResponse>(response);
        }
    }
}
