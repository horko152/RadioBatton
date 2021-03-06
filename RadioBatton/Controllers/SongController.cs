﻿using System;
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
    public class SongController : ControllerBase
    {
        private readonly SongRepository songRepository;
        public SongController(SongRepository songRepository)
        {
            this.songRepository = songRepository;
        }
        // GET: api/Songs
        ///<summary>
        ///Get All Songs
        ///</summary>
        [HttpGet]
        [Route("~/api/songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await songRepository.GetAll().ToListAsync();
        }

        // GET: api/Song/5
        ///<summary>
        ///Get Song By SongId
        ///</summary>
        [HttpGet("{id:int}")]
        public ActionResult<Song> GetSong([FromRoute] int id)
        {
            var song = songRepository.GetById(id);
            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // GET: api/User/4/songs
        ///<summary>
        /// Get Songs By User
        ///</summary>
        [HttpGet("{id:int}")]
        public IQueryable<Song> GetSongsByUser([FromRoute] int id)
        {
            var songs = songRepository.GetSongsByUserId(id);
            if (songs == null)
            {
                throw new ArgumentException();
            }

            return songs;
        }

        // PUT: api/Song/5
        ///<summary>
        ///Update Song
        ///</summary>
        [HttpPut("{id}")]
        public IActionResult UpdateSong(int id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }
            try
            {
                songRepository.UpdateSong(id, song);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/Song
        ///<summary>
        ///Create Song
        ///</summary>
        [HttpPost]
        public ActionResult<Song> CreateSong(Song song)
        {
            songRepository.CreateSong(song);
            return CreatedAtAction("GetSong", new { id = song.Id }, song);
        }

        // DELETE: api/Song/5
        ///<summary>
        ///Delete Song
        ///</summary>
        /// <param name="id"></param> 
        [HttpDelete("{id}")]
        public void DeleteSong(int id)
        {
            songRepository.Delete(id);
        }

        private bool SongExists(int id)
        {
            return songRepository.GetAll().Any(e => e.Id == id);
        }
    }
}