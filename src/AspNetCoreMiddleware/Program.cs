using AspNetCoreMiddleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//定义一个中间件
 static RequestDelegate Middleware(RequestDelegate next)
=>async (HttpContext context) =>
{
    Console.WriteLine("开始执行中间件A");
    //...
    await context.Response.WriteAsync("A=>");
    await next(context);
    Console.WriteLine("再次执行中间件A");
};

var Middleware2 = async (HttpContext context, RequestDelegate next) =>
  {
      Console.WriteLine("开始执行中间件B");
      await context.Response.WriteAsync("B=>");
      await next(context);
      Console.WriteLine("再次执行中间件B");
  };

app.Map("/test", builder =>
{
    builder.Use(Middleware);
    builder.Use(Middleware2);
    builder.Use(async (context, next) =>
    {
        Console.WriteLine("开始执行中间件C");
        await context.Response.WriteAsync("C=>");
        await next(context);
        Console.WriteLine("再次执行中间件C");
    });
    builder.UseMiddleware<Middleware4>();
    builder.Run(async (context) =>
    {
        Console.WriteLine("开始执行终结中间件D");
        await context.Response.WriteAsync("D=>");
    });
});
app.Run();
