using Microsoft.AspNetCore.Mvc;

namespace WebApplicationCancellationToken2.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get(CancellationToken cancellation)
        {
            await DownloadAsync("https://www.baidu.com",500, cancellation);
            return Ok();
        }
        static async Task DownloadAsync(string url, int n, CancellationToken cancellation)
        {
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    await client.GetStringAsync(url);
                    Console.WriteLine($"{DateTime.Now}");
                    if (cancellation.IsCancellationRequested)
                    {
                        Console.WriteLine("请求被取消");
                        break;
                    }
                    //cancellation.ThrowIfCancellationRequested();
                }
            }
        }
    }
}