using ConsoleAppLog;
using Exceptionless;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using SystemServices;

ExceptionlessClient.Default.Startup("ReakGqd4zGSGFHpONRZuIX11y5yoDaj1SU3nlXkQ");

ServiceCollection services = new ServiceCollection();
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console(new JsonFormatter())
    .WriteTo.Exceptionless()
    .CreateLogger();

services.AddLogging(builder =>
{
    //builder.AddConsole(opt =>{});
    //builder.SetMinimumLevel(LogLevel.Trace);//如果用了三方的一些日志系统如NLog，这个不要设置
    //builder.AddEventLog();
    //builder.AddNLog();
    builder.AddSerilog();
});
services.AddScoped<Test1>();
services.AddScoped<Test2>();
IServiceProvider serviceProvider = services.BuildServiceProvider();
Test1 test1 = serviceProvider.GetRequiredService<Test1>();
Test2 test2 = serviceProvider.GetRequiredService<Test2>();
test1.Test();
test2.Test();
//for (int i = 0; i < 1000; i++)
//{
//    test2.Test();
//}
Console.ReadLine();