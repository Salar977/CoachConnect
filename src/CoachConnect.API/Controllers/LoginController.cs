using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

    private string GenerateJSONWebToken(User userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          null,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private User? AuthenticateUser(LoginDTO loginDto)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == loginDto.Username);

        if (user != null)
        {
            if (BCrypt.Net.BCrypt.Verify(loginDto.Password, user.HashedPassword))
            {
                return user;
            }
        }

        // Authentication fail return null
        return null;
    }
}
