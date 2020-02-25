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
    public class GenreController : ControllerBase
    {
        private readonly GenreRepository genreRepository;

        public GenreController(GenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        // GET: api/Genres
        ///<summary>
        ///Get All Genres
        ///</summary>
        [HttpGet]
        [Route("~/api/genres")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await genreRepository.GetAll().ToListAsync();
        }

        // GET: api/Genre/5
        ///<summary>
        ///Get Genre By GenreId
        ///</summary>
        [HttpGet("{id:int}")]
        public ActionResult<Genre> GetGenre([FromRoute] int id)
        {
            var genre = genreRepository.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        // PUT: api/Genre/5
        ///<summary>
        ///Update Genre
        ///</summary>
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, Genre genre)
        {
            if (id != genre.Id)
            {
                return BadRequest();
            }
            try
            {
                genreRepository.UpdateGenre(id, genre);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // POST: api/Genre
        ///<summary>
        ///Create Genre
        ///</summary>
        [HttpPost]
        public ActionResult<Genre> CreateGenre(Genre genre)
        {
            genreRepository.CreateGenre(genre);
            return CreatedAtAction("GetGenre", new { id = genre.Id }, genre);
        }

        // DELETE: api/Genre/5
        ///<summary>
        ///Delete Genre
        ///</summary>
        /// <param name="id"></param> 
        [HttpDelete("{id}")]
        public void DeleteGenre(int id)
        {
            genreRepository.Delete(id);
        }

        private bool GenreExists(int id)
        {
            return genreRepository.GetAll().Any(e => e.Id == id);
        }
    }
}