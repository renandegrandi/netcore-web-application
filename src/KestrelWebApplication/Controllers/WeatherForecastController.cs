using KestrelWebApplication.Core.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KestrelWebApplication.Controllers
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
        private readonly IConfiguration _config;
        private readonly ExampleConfig _exampleConfig;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IConfiguration config,
            IOptions<ExampleConfig> exampleConfig)
        {
            _logger = logger;
            _config = config;
            _exampleConfig = exampleConfig.Value;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("configuration")]
        public IActionResult GetConfiguration() 
        {
            
            var keyForValue = "Logging:LogLevel:Default";
            var KeyForObjectValue = "Logging:LogLevel";

            var value = _config[keyForValue];
            var section = _config.GetSection(KeyForObjectValue);
            var existsSection = section.Exists();

            var sectionChildren = section.GetChildren();

            foreach (var itemKey in sectionChildren.Select((item) => item.Key))
            {
                var itemValue = section[itemKey];
            }

            var valueInSection = section["Default"];

            return Ok(new {
                _exampleConfig,
                value,
                existsSection,
                valueInSection
            });
        }
    }
}