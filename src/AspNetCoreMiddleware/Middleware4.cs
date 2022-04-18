namespace AspNetCoreMiddleware
{
    public class Middleware4
    {
        private readonly RequestDelegate _next;

        public Middleware4(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("开始执行中间件E");
            await _next(context);
            Console.WriteLine("结束执行中间件E");
        }
    }
}
