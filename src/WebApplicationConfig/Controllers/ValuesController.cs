using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace WebApplicationConfig.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<AppConfig> _optionsAppConfig;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public ValuesController(IWebHostEnvironment env, IOptions<AppConfig> optionsAppConfig, IConnectionMultiplexer connectionMultiplexer)
        {
            _env = env;
            _optionsAppConfig = optionsAppConfig;
            _connectionMultiplexer = connectionMultiplexer;
        }

        [HttpGet]
        public IActionResult Test()
        {
            var environmentVars=Environment.GetEnvironmentVariables();
            var envPath = Environment.GetEnvironmentVariable("PATH");
            var haha = Environment.GetEnvironmentVariable("haha");
            return Ok(environmentVars);
        }
        [HttpGet("appConfig")]
        public IActionResult GetAppConfig()
        {
            var redisPing = _connectionMultiplexer.GetDatabase(0).StringSetAndGet("testString","MyTestString").ToString();
            var appConfig = _optionsAppConfig.Value;
            return Ok(new { appConfig , redisPing });
        }
    }
}
