using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.Queries;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;

namespace StudySharp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
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
    }
}
