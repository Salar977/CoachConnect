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

    public LoginController(IConfiguration config, CoachConnectDbContext dbContext)
    {
        _config = config;
        _dbContext = dbContext;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] LoginDTO loginDto)
    {
        IActionResult response = Unauthorized("Not Authorized");
        var user = AuthenticateUser(loginDto);

        if (user != null)
        {
            var tokenString = GenerateJSONWebToken(user);
            response = Ok(new { token = tokenString });
        }

        return response;
    }

    private string GenerateJSONWebToken(Login userOrCoach)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
    

        // Add UserId claim if the userOrCoachInfo is a User or Coach
        if (userOrCoach is User user)
        {
            claims.Add(new Claim("UserId", user.Id.ToString()));
            claims.Add(new Claim("UserName", user.Email.ToString()));
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("Role", role.JwtRoleId.ToString()));
            }
        }
        else if (userOrCoach is Coach coach)
        {
            claims.Add(new Claim("UserId", coach.Id.ToString()));
            claims.Add(new Claim("UserName", coach.Id.ToString()));
            //foreach (var role in coach.Roles)
            //{
            //    claims.Add(new Claim("Role", role.JwtRoleId.ToString()));
            //}
        }

        // admin claims missing

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
