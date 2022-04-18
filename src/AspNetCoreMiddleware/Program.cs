using AspNetCoreMiddleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//����һ���м��
 static RequestDelegate Middleware(RequestDelegate next)
=>async (HttpContext context) =>
{
    Console.WriteLine("��ʼִ���м��A");
    //...
    await context.Response.WriteAsync("A=>");
    await next(context);
    Console.WriteLine("�ٴ�ִ���м��A");
};

var Middleware2 = async (HttpContext context, RequestDelegate next) =>
  {
      Console.WriteLine("��ʼִ���м��B");
      await context.Response.WriteAsync("B=>");
      await next(context);
      Console.WriteLine("�ٴ�ִ���м��B");
  };

app.Map("/test", builder =>
{
    builder.Use(Middleware);
    builder.Use(Middleware2);
    builder.Use(async (context, next) =>
    {
        Console.WriteLine("��ʼִ���м��C");
        await context.Response.WriteAsync("C=>");
        await next(context);
        Console.WriteLine("�ٴ�ִ���м��C");
    });
    builder.UseMiddleware<Middleware4>();
    builder.Run(async (context) =>
    {
        Console.WriteLine("��ʼִ���ս��м��D");
        await context.Response.WriteAsync("D=>");
    });
});
app.Run();
