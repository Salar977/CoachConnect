using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoachConnect.API.Middleware;

public class JwtExtractionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<JwtExtractionMiddleware> _logger;

    public JwtExtractionMiddleware(RequestDelegate next, ILogger<JwtExtractionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            _logger.LogInformation("Getting Id and Username from token");

            var jwtToken = new JwtSecurityToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;

            context.Items["UserId"] = userId;
            context.Items["UserName"] = userName;
        }

        await _next(context);
    }
}
