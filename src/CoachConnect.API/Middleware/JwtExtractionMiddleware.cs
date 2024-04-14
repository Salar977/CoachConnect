using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoachConnect.API.Middleware;

public class JwtExtractionMiddleware
{
    private readonly RequestDelegate _next;

    public JwtExtractionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var jwtToken = new JwtSecurityToken(token);
            var roles = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;

            // Add roles, userId, and userName to HttpContext for use in controllers
            context.Items["Roles"] = roles;
            context.Items["UserId"] = userId;
            context.Items["UserName"] = userName;
        }

        await _next(context);
    }
}
