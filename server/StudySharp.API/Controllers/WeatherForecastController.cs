using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.EmailService;
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
        public async Task<OperationResult<Tag>> Get([FromQuery] GetTagByIdQuery getTagByIdQuery)
        {
            return await _mediator.Send<OperationResult<Tag>>(getTagByIdQuery);
        }

        [HttpPost]
        public async Task<OperationResult> Add([FromBody] AddTagCommand addTagCommand)
        {
            return await _mediator.Send<OperationResult>(addTagCommand);
        }

        [HttpPut]
        public async Task<OperationResult> SendMessage([FromBody] Email email)
        {
            await _emailService.SendEmailAsync(email.To, "Test", email.Message);
            return OperationResult.Ok();
        }
    }
}
