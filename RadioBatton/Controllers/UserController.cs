using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RadioBatton.Security;

namespace RadioBatton.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public UserController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: api/User
        ///<summary>
		///Get All Users
		///</summary>
		[HttpGet]
        [Route("~/api/users")]
		[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await userRepository.GetAll().ToListAsync();
        }

        // GET: api/User/5
        ///<summary>
		///Get User By UserId
		///</summary>
        [HttpGet("{id:int}")]
        public ActionResult<User> GetUser([FromRoute] int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        ///<summary>
		///Update User
		///</summary>
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                userRepository.UpdateUser(id, user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        ///<summary>
		///Create User
		///</summary>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<User> CreateUser(User user)
        {
            //if(user == null) 
            //{ 
            userRepository.CreateUser(user);
            //var result = new AuthenticationController();
            //var credentials = new Credentials(user.UserName, user.Password);
            //return Json(result.GenerateToken(credentials));
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
            //}
            //return BadRequest("User undifined");
        }

        // DELETE: api/User/5
        ///<summary>
		///Delete User
		///</summary>
		/// <param name="id"></param> 
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            userRepository.Delete(id);
        }

        private bool UserExists(int id)
        {
            return userRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
