using CoachConnect.API.Middleware;
using CoachConnect.BusinessLayer;
using CoachConnect.DataAccess;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers()
    .RequireRateLimiting("fixed");

app.Run();