var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(setup =>
{
    setup.Configuration = "94.191.83.150:6379,password=2439739932";
    setup.InstanceName = "spyCache_";//����ǰ׺���������ݻ���
    setup.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
    {
        DefaultDatabase = 5,
        Password = "2439739932",
        EndPoints =
                    {
                        {"94.191.83.150" },
                    }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors();
//���÷����"��Ӧ����"
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
