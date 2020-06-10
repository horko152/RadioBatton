using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Enums;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RadioBatton.Security;

namespace RadioBatton.Controllers
{
    [Produces("text/html")]
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly IConfiguration _config;

        public AuthenticationController(UserRepository userRepository, IConfiguration config)
        {
            _config = config;
            this.userRepository = userRepository;
        }
        // POST: api/login
        [HttpPost]

        [AllowAnonymous]
        public IActionResult GenerateToken([FromBody] Credentials credentials)
        {

            var user = userRepository.GetByUsername(credentials.Username);

            if (user != null)
            {
                if (user.Password.Equals(credentials.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                        };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationOptions.SIGNING_KEY));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                         issuer: "https://localhost:44360/",
                        audience: "https://localhost:44360/",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: creds);

                    var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                    var responce = new
                    {
                        token = encodedToken,
                        id = user.Id,
                        username = user.UserName,
                        email = user.Email,
                        role = user.Role.ToString()
                    };
					Request.HttpContext.Response.Headers.Add("Authorization", "Bearer " + encodedToken);
					//Request.HttpContext.Response.Body.Write();
					return Json(responce);
                }
                return BadRequest(new { errorText = "Invalid username or password" });
            }
            return BadRequest("Could not create token");
        }

        [AllowAnonymous]
        [HttpPost("/registration")]
        public IActionResult Registation([FromBody] RegistrCredentials credentials)
        {
            User user = userRepository.GetByEmail(credentials.Email);
            if (user == null)
            {
                userRepository.CreateUser(new User { UserName = credentials.Username, Email = credentials.Email, Password = credentials.Password, Role = Role.User });
                return GenerateToken(new Credentials { Username = credentials.Username, Password = credentials.Password });
            }
            else
            {
                return BadRequest("Email is used");
            }
        }
    }
}