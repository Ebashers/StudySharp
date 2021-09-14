using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.DomainServices;

namespace StudySharp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorchinggg",
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly StudySharpDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, StudySharpDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<OperationResult> Get()
        {
            await _context.Tags.AddAsync(new Tag { Name = "CoolCourse" });
            await _context.SaveChangesAsync();
            return OperationResult.Ok(_context.Tags.ToList());
        }
    }
}
