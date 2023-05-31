using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [EnableCors(Constants.StandardCorsPolicyName)]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = Random.Shared.Next(0, 10),
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        // this is added for the corresponding put call, to enable the preflight request as per cors
        [HttpOptions("{id}")]
        public IActionResult PreflightRoute(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}")]
        [EnableCors(Constants.StandardCorsPolicyName)]
        public WeatherForecast Put(int id)
        {
            return new WeatherForecast
            {
                Id = id,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpGet]
        [Route("Details")]
        // note that this endpoint is not enabled with cors
        public IEnumerable<WeatherForecast> GetDetails()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = Random.Shared.Next(0, 10),
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}