using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiFilter
{
    public class LogExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            await File.AppendAllTextAsync("./error.log", context.Exception.ToString());
        }
    }
}
