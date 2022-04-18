// See https://aka.ms/new-console-template for more information
using ConsoleAppConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
configurationBuilder
    .AddJsonFile("appsettion.json", optional: true, reloadOnChange: true)
    .AddCommandLine(args)
    .AddEnvironmentVariables("MyEnv_")//加一个前缀，避免和其他环境变量冲突如：MyEnv_Name
    .AddUserSecrets<Program>();

//configurationBuilder.Add(new IConfigurationSource());
 IConfigurationRoot configurationRoot = configurationBuilder.Build();


string name = configurationRoot["name"];
string address = configurationRoot.GetSection("proxy:address").Value;
Console.WriteLine(name);
Proxy proxy = configurationRoot.GetSection("proxy").Get<Proxy>();
Console.WriteLine(proxy);
Config config=configurationRoot.Get<Config>();


ServiceCollection services = new ServiceCollection();
services.AddOptions().Configure<Config>(e => configurationRoot.Bind(e))
    .Configure<Proxy>(e=>configurationRoot.GetSection("proxy").Bind(e));
services.AddScoped<TestController>();

IServiceProvider serviceProvider= services.BuildServiceProvider();
var c=serviceProvider.GetRequiredService<TestController>();
c.Test();

while (true)
{
    Console.ReadLine();
    using(var scope = serviceProvider.CreateScope())
    {
        var controller=scope.ServiceProvider.GetRequiredService<TestController>();
        controller.Test();
    }
}