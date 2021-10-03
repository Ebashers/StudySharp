using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.EmailService;
using StudySharp.ApplicationServices.EmailService.Constants;
using StudySharp.ApplicationServices.EmailService.Models;
using StudySharp.ApplicationServices.Queries;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;

namespace StudySharp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator, IEmailService emailService)
        {
            _logger = logger;
            _mediator = mediator;
            _emailService = emailService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<OperationResult<Tag>> Get([FromQuery] GetTagByIdQuery getTagByIdQuery)
        {
            return await _mediator.Send(getTagByIdQuery);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<OperationResult> Add([FromBody] AddTagCommand addTagCommand)
        {
            return await _mediator.Send(addTagCommand);
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<OperationResult> SendMessage()
        {
            await _emailService.SendEmailAsync(
                EmailTemplates.Default.Build("Text, token, url, etc."),
                new EmailContact("User First and Last name", "kotohomka@gmail.com"));
            return OperationResult.Ok();
        }
    }
}
