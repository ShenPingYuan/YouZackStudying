// See https://aka.ms/new-console-template for more information
using DIExample;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");
ServiceCollection services = new ServiceCollection();
services.AddScoped<IStudent, Student>();
IServiceProvider serviceProvider = services.BuildServiceProvider();
IStudent student = serviceProvider.GetRequiredService<IStudent>();
student.Name = "Michael Shen";
student.Say();

IStudent student2 = serviceProvider.GetRequiredService<IStudent>();
Console.WriteLine(Object.ReferenceEquals(student,student2));

//services.AddSingleton<IStudent, Student>();
IStudent student3 = serviceProvider.GetRequiredService<IStudent>();
IStudent student4 = serviceProvider.GetRequiredService<IStudent>();
Console.WriteLine(Object.ReferenceEquals(student3, student4));

using(IServiceScope scope = serviceProvider.CreateScope())
{
    var student5= scope.ServiceProvider.GetService<IStudent>();
    var student6= scope.ServiceProvider.GetService<IStudent>();
    Console.WriteLine(Object.ReferenceEquals(student5, student6));
}

using (IServiceScope scope2 = serviceProvider.CreateScope())
{
    var student7 = scope2.ServiceProvider.GetService<IStudent>();
}

//GetService

IStudent student8 = serviceProvider.GetService<IStudent>();
IStudent student9 =(IStudent)serviceProvider.GetService(typeof(IStudent));

IStudent student10 = serviceProvider.GetRequiredService<IStudent>();//如果找不到服务就抛异常

IEnumerable<IStudent> students = serviceProvider.GetServices<IStudent>();