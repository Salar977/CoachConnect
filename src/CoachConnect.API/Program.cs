using CoachConnect.API.Middleware;
using CoachConnect.BusinessLayer;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.BusinessLayer.Services;
using CoachConnect.DataAccess;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.DataAccess.Repositories;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using CoachConnect.API.Extensions;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.RegisterMappers();

// Rate Limiter - Simple rate limiter with fixed 5 seconds for each request otherwise 429: Too Many Requests // Husk denne m� flyttes til appsettings
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 1;
        options.Window = TimeSpan.FromSeconds(5);
        options.QueueLimit = 0;
        rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    });
});

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
        };
    });
    services.AddMvc(); 
}

ConfigureServices(builder.Services, builder.Configuration);

builder.Services.AddBusinessLayer();
builder.Services.AddDataAccess(builder.Configuration);

builder.Services.AddScoped<GlobalExceptionMiddleware>();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseRateLimiter();

app.UseHttpsRedirection();
app.UseMiddleware<JwtExtractionMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers()
    .RequireRateLimiting("fixed");

app.Run();