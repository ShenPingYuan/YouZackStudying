using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiFilter
{
    public class MyExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public MyExceptionFilter(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            //context.Exception
            //context.ExceptionHandled = true;//这个值为true，则其他ExceptionFilter不会再执行
            //context.Result//返回给客户端的IActionResult
            string message = string.Empty;
            if (_hostEnvironment.IsDevelopment())
            {
                message = context.Exception.Message;
            }
            else
            {
                message = "服务器出错";
            }
            context.HttpContext.Response.StatusCode = 500;
            context.Result = new ObjectResult(message);
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
