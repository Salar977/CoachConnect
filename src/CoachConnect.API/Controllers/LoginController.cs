using CoachConnect.API.Controllers;
using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Controllers;

[Route("api/v1/login")]
[ApiController]
public class LoginController : Controller
{
    private IConfiguration _config;
    private readonly CoachConnectDbContext _dbContext;
    private readonly ILogger<UsersController> _logger;

    public LoginController(IConfiguration config, CoachConnectDbContext dbContext, ILogger<UsersController> logger)
    {
        _config = config;
        _dbContext = dbContext;
        _logger = logger;
    }
    
    [AllowAnonymous]
    // POST https://localhost:7036/api/v1/login
    [HttpPost]
    public IActionResult Login([FromBody] LoginDTO loginDto)
    {
        _logger.LogDebug("User logging in: {username}", loginDto.Username);

        IActionResult response = Unauthorized("Not Authorized");
        var user = AuthenticateUser(loginDto);

        if (user != null)
        {
            var tokenString = GenerateJSONWebToken(user);
            response = Ok(new { token = tokenString });
        }

        return response;
    }

    public enum UserRole
    {
        Admin = 1,
        Coach = 2,
        User = 3    
    }


    private string GenerateJSONWebToken(Login userOrCoach)
    {
        _logger.LogDebug("Generating Token");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();

        if (userOrCoach is User user)
        {
            var userRoles = _dbContext.Jwt_user_roles.Where(u => u.UserName == user.Email);

            claims.Add(new Claim("UserId", user.Id.ToString()));
            claims.Add(new Claim("UserName", user.Email.ToString()));

            foreach (var role in userRoles)
            {
                var enumRole = (UserRole)role.JwtRoleId;
                claims.Add(new Claim(ClaimTypes.Role, enumRole.ToString()));
            }        
        }
        else if (userOrCoach is Coach coach)
        {
            var userRoles = _dbContext.Jwt_user_roles.Where(u => u.UserName == coach.Email);

            claims.Add(new Claim("UserId", coach.Id.ToString()));
            claims.Add(new Claim("UserName", coach.Email.ToString()));

            foreach (var role in userRoles)
            {
                var enumRole = (UserRole)role.JwtRoleId;
                claims.Add(new Claim(ClaimTypes.Role, enumRole.ToString()));
            }
        }       

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private Login? AuthenticateUser(LoginDTO loginDto)
    {
        _logger.LogDebug("Authenticating user: {username}", loginDto.Username);

        var user = _dbContext.Users.FirstOrDefault(u => u.Email.Equals(loginDto.Username));
        if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.HashedPassword))
        {
            return user; 
        }
        
        var coach = _dbContext.Coaches.FirstOrDefault(c => c.Email.Equals(loginDto.Username));
        if (coach != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, coach.HashedPassword))
        {
            return coach; 
        }

        // Authentication failed
        return null;
    }
}
