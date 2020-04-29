using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
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
    public class AuthenticationController : ControllerBase
    {
        private UserRepository userRepository;
        private IConfiguration _config;

        public AuthenticationController(UserRepository userRepository, IConfiguration config)
        {
            _config = config;
            this.userRepository = userRepository;
        }
        // POST: api/login
        ///<summary>
        ///Get Token
        ///</summary>
        /// <response code="200">OK</response>
        /// <response code="201">Token created</response>
        /// <response code="400">Something wrong</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public ActionResult GenerateToken([FromBody] Credentials credentials)
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
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: creds);

                    var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                    Request.HttpContext.Response.Headers.Add("Authorization", "Bearer " + encodedToken);

                    return Ok();
                }
            }
            return BadRequest("Could not create token");
        }
    }
}