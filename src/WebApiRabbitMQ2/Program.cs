using System.Reflection;
using Zack.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<IntegrationEventRabbitMQOptions>(o =>
{
    o.HostName = "localhost";
    o.ExchangeName = "apiExchangeEventBusDemo1";
});
builder.Services.AddEventBus("queue1", Assembly.GetExecutingAssembly());//相应事件的那些程序集
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseEventBus();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
