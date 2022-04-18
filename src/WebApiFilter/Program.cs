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
    opt.Filters.Add<RateLimitFilter>();//��������,һ�����ط��������٣������������

    opt.Filters.Add<MyExceptionFilter>();//��ִ��
    opt.Filters.Add<LogExceptionFilter>();//��ִ��

    opt.Filters.Add<MyActionFilter>();//��
    //opt.Filters.Add<MyActionFilter2>();��

    opt.Filters.Add<TransactionScopeFilter>();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var sqlServerConnectionString = "Server=.;Database=webapidemo2;Trusted_Connection=true;MultipleActiveResultSets=true";
    options.UseSqlServer(sqlServerConnectionString);
    //options.UseSqlServer(sqlServerConnectionString,option=>option.MigrationsAssembly(migrationAssembly));
    options.EnableSensitiveDataLogging(true);//���������ݼ�¼
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
