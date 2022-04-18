namespace AspNetCoreMiddleware
{
    public class Middleware5 : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
