using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.Configure<MvcOptions>(opt =>
{
    opt.Filters.Add<RateLimitFilter>();//请求限速,一般网关服务器限速，不在这儿限速

    opt.Filters.Add<MyExceptionFilter>();//后执行
    opt.Filters.Add<LogExceptionFilter>();//先执行

    opt.Filters.Add<MyActionFilter>();//先
    //opt.Filters.Add<MyActionFilter2>();后

    opt.Filters.Add<TransactionScopeFilter>();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var sqlServerConnectionString = "Server=.;Database=webapidemo2;Trusted_Connection=true;MultipleActiveResultSets=true";
    options.UseSqlServer(sqlServerConnectionString);
    //options.UseSqlServer(sqlServerConnectionString,option=>option.MigrationsAssembly(migrationAssembly));
    options.EnableSensitiveDataLogging(true);//打开敏感数据记录
});

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
