using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiSignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR().AddStackExchangeRedis("94.191.83.150:6379, password = 2439739932", option =>
{
    //option.Configuration.EndPoints.Add("94.191.83.150,6379");
    //option.Configuration.Password = "2439739932";
    option.Configuration.ChannelPrefix = "SignalR_";
    option.Configuration.DefaultDatabase = 10;
});
builder.Services.AddCors(builder =>
{
    builder.AddPolicy("default", policy =>
     {
         policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
     });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        string key = "kfsl;dakgjkas;ldfjERSadfkaT#RA%dfasld";
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = securityKey
        };
        options.Events = new JwtBearerEvents()
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/Hubs"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
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
app.UseCors("default");

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatRoomHub>("/Hubs/ChatRoomhub");
app.MapControllers();

app.Run();
