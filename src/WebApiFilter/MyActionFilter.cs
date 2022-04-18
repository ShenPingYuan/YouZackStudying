using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiFilter
{
    public class MyActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($"{context.ActionDescriptor.DisplayName}执行之前");
            ActionExecutedContext actionExecutedContext = await next();
            Console.WriteLine($"{context.ActionDescriptor.DisplayName}执行之后");
            if (actionExecutedContext != null && actionExecutedContext.Exception != null)
            {
                Console.WriteLine("发生异常了");
            }
            else
            {
                Console.WriteLine($"{actionExecutedContext!.ActionDescriptor.DisplayName}执行成功");
            }
        }
    }
}
