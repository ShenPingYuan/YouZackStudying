// See https://aka.ms/new-console-template for more information
using ConfigServices;
using MailServices;
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services=new ServiceCollection();
//services.AddScoped<IConfigService, EnvVarConfigService>();
services.AddScoped(typeof(IConfigService), (serviceProvider) => new IniFileConfigService { FilePath = @"./config.ini" });
services.AddScoped<IMailService, MailService>();
services.AddConsoleLog();

IServiceProvider serviceProvider = services.BuildServiceProvider();

var mailService= serviceProvider.GetRequiredService<IMailService>();
mailService.Send("测试邮件", "2439739932@qq.com", "hello,michaelshen");