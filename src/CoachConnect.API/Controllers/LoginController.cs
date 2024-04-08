using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JWTAuthentication.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            IActionResult response = Unauthorized();
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private User AuthenticateUser(LoginDTO loginDto)
        {
            User user = null;

            //Validate the User Credentials
            //Demo Purpose, I have Passed HardCoded User Information


            //if (login.Username == "Jignesh")
            //{
            //    user = new User { Username = "Jignesh Trivedi", EmailAddress = "test.btest@gmail.com" };
            //}
            //return user;
            return null;
        }
    }
}
