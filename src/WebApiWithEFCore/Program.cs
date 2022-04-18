using EFCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zack.Commons;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var migrationAssembly=typeof(ApplicationDbContext).Assembly.GetName().Name;
//Scope
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var sqlServerConnectionString = "Server=.;Database=webapidemo;Trusted_Connection=true;MultipleActiveResultSets=true";
    options.UseSqlServer(sqlServerConnectionString);
    //options.UseSqlServer(sqlServerConnectionString,option=>option.MigrationsAssembly(migrationAssembly));
    options.EnableSensitiveDataLogging(true);//打开敏感数据记录
});
var dbContextsAssembly = Assembly.Load("EFCore");//ReflectionHelper.GetAllReferencedAssemblies();

builder.Services.AddAllDbContexts(builder =>
{
    var sqlServerConnectionString = "Server=.;Database=webapidemo;Trusted_Connection=true;MultipleActiveResultSets=true";
    builder.UseSqlServer(sqlServerConnectionString);
},new Assembly[] { dbContextsAssembly });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
