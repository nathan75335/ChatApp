using ChatApp.API.Extensions;
using ChatApp.API.Middlewares;
using ChatApp.API.Profiles;
using ChatApp.BusinessLogic.Configurations;
using ChatApp.BusinessLogic.Services.Implementations;
using ChatApp.BusinessLogic.Services.Interfaces;
using ChatApp.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddDatabaseServices(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    })
    .AddScoped<IUserService, UserService>()
    .AddScoped<IMessageService, MessageService>()
    .AddScoped<IContactService, ContactService>()
    .AddScoped<IAuthenticateService, AuthenticateService>()
    .AddAutoMapper(typeof(ApplicationProfile))
    .AddJwtAuthentication(builder.Configuration)
    .AddTransient<CustomExceptionMiddleware>()
    .AddCors(options => options.AddPolicy("Any", opts => opts
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Any");

app.UseCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
