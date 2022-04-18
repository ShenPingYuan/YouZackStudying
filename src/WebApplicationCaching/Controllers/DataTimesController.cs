using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplicationCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataTimesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<DataTimesController> _logger;

        public DataTimesController(IMemoryCache memoryCache, ILogger<DataTimesController> logger, IDistributedCache distributedCache)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _distributedCache = distributedCache;
        }

        [HttpGet("responseCache")]
        [ResponseCache(Duration =10)]//响应缓存
        public DateTime NowWithResponseCache()
        {
            return DateTime.Now;
        }
        [HttpGet("memoryCache")]
        public async Task<ActionResult<string>> NowWithMemoryCache()
        {
            _logger.LogInformation($"开始执行{nameof(NowWithMemoryCache)}");
            _logger.LogInformation($"从缓存中读取数据");
            string dateTimeNow = await _memoryCache.GetOrCreateAsync("dateTimeNow", e =>
               {
                   _logger.LogInformation($"缓存中不存在数据");
                   _logger.LogInformation("从数据库中读取数据");
                   //e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);//绝对过期时间10s,有可能缓存雪崩
                   e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.Next(10,15));//绝对过期时间为10-15s的随机值，防止缓存雪崩
                   e.SlidingExpiration=TimeSpan.FromSeconds(2);//滑动过期时间2s
                   return Task.FromResult(DateTime.Now.ToString("yyyy-MM-dd"));
               });
            return dateTimeNow;
        }
        [HttpGet("distributedCache")]
        public async Task<ActionResult<string?>> NowWithDistributedCache()
        {
            _logger.LogInformation($"开始执行{nameof(NowWithDistributedCache)}");
            _logger.LogInformation($"从redis分布式缓存中读取数据");
            string? dateTimeNow=await _distributedCache.GetStringAsync("dateTimeNow");
            if (dateTimeNow == null)
            {
                _logger.LogInformation($"缓存中不存在数据");
                _logger.LogInformation("从数据库中读取数据");
                dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
                await _distributedCache.SetStringAsync("dateTimeNow", dateTimeNow,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow=TimeSpan.FromSeconds(Random.Shared.Next(10,15)),
                        SlidingExpiration=TimeSpan.FromSeconds(4),
                    });
            }
            return dateTimeNow;
        }
    }
}
