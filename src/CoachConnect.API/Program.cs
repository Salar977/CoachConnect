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
using System.Text;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddSwaggerWithBasicAuthentication();

builder.RegisterMappers();

// Rate Limiter - Simple rate limiter with fixed 5 seconds for each request otherwise 429: Too Many Requests
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

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers()
    .RequireRateLimiting("fixed");

app.Run();