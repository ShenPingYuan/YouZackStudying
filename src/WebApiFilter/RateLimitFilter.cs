using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace WebApiFilter
{
    public class RateLimitFilter : IAsyncActionFilter
    {
        private readonly IMemoryCache _memoryCache;
        public RateLimitFilter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
         {
            string? remoteIP = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            string cacheKey = $"LastVisitTick_{remoteIP}";
            long? lastTick=_memoryCache.Get<long?>(cacheKey);
            if (!lastTick.HasValue || Environment.TickCount64 - lastTick > 1000)
            {
                _memoryCache.Set(cacheKey, Environment.TickCount64,TimeSpan.FromSeconds(10));
                return next(); 
            }
            else
            {
                context.Result = new ObjectResult("访问太频繁") { StatusCode = 429 };
                return Task.CompletedTask;
            }
        }
    }
}
