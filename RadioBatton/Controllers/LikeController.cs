using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RadioBatton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly LikeRepository likeRepository;

        public LikeController(LikeRepository likeRepository)
        {
            this.likeRepository = likeRepository;
        }

        // GET: api/Likes
        ///<summary>
        ///Get All Likes
        ///</summary>
        [HttpGet]
        [Route("~/api/likes")]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        {
            return await likeRepository.GetAll().ToListAsync();
        }

        // GET: api/Like/5
        ///<summary>
        ///Get Like By LikeId
        ///</summary>
        [HttpGet("{id:int}")]
        public ActionResult<Like> GetLike([FromRoute] int id)
        {
            var like = likeRepository.GetById(id);
            if (like == null)
            {
                return NotFound();
            }

            return like;
        }

        //// GET: api/Song/4/likes
        /////<summary>
        ///// Get Likes By Song
        /////</summary>
        //[HttpGet("{id:int}")]
        //public IQueryable<Like> GetLikesBySong([FromRoute] int id)
        //{
        //    var likes = likeRepository.GetLikesBySongId(id);
        //    if (likes == null)
        //    {
        //        throw new ArgumentException();
        //    }

        //    return likes;
        //}


        // PUT: api/Like/5
        ///<summary>
        ///Update Like
        ///</summary>
        [HttpPut("{id}")]
        public IActionResult UpdateLike(int id, Like like)
        {
            if (id != like.Id)
            {
                return BadRequest();
            }
            try
            {
                likeRepository.UpdateLike(id, like);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeExists(id))
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

        // POST: api/Like
        ///<summary>
        ///Create Like
        ///</summary>
        [HttpPost]
        public ActionResult<Like> CreateLike(Like like)
        {
            likeRepository.CreateLike(like);
            return CreatedAtAction("GetLike", new { id = like.Id }, like);
        }

        // DELETE: api/Like/5
        ///<summary>
        ///Delete Like
        ///</summary>
        /// <param name="id"></param> 
        [HttpDelete("{id}")]
        public void DeleteLike(int id)
        {
            likeRepository.Delete(id);
        }

        private bool LikeExists(int id)
        {
            return likeRepository.GetAll().Any(e => e.Id == id);
        }
    }
}