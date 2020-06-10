using DAL.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
	public class SongRepository : GeneralRepository<Song>
	{
		private readonly LikeRepository likeRepository;
		private readonly UserRepository userRepository;
		public SongRepository(RadioBattonDbContext DbContext, LikeRepository likeRepository, UserRepository userRepository) : base(DbContext)
		{
			this.likeRepository = likeRepository;
			this.userRepository = userRepository;
			this.DbContext = DbContext;
		}

		public void CreateSong(Song Entity)
		{
			DbContext.Add(Entity);
			DbContext.SaveChanges();
		}

		public SongMain GetSongForMainPageById(int id)
		{
			var song = GetById(id);
			if (song == null)
			{
				throw new ArgumentException();
			}
			SongMain songinfo = new SongMain()
			{
				Id = song.Id,
				Artist = song.Artist,
				Name = song.SongName,
				Author = userRepository.GetById(song.UserId).UserName,
				FilePath = song.File
			};

			return songinfo;
		}

		//public IQueryable<Song> GetSongsByArtist(string artist)
		//{
		//	IQueryable<Song> songs = DbContext.Songs.ToList().Where(x => x.Artist == artist).AsQueryable();
		//	if (songs != null)
		//	{
		//		return songs;
		//	}
		//	else
		//	{
		//		throw new ArgumentException();
		//	}
		//}

		public IQueryable<Song> GetSongsByUserId(int id)
		{
			IQueryable<Song> songs = DbContext.Songs.ToList().Where(x => x.UserId == id).AsQueryable();
			if (songs != null)
			{
				return songs;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public IQueryable<SongInfo> GetSongInfosByUser(int id)
		{
			var songs = GetSongsByUserId(id);
			var songInfos = new List<SongInfo>();
			if (songs == null)
			{
				throw new ArgumentException();
			}
			foreach (var item in songs)
			{
				songInfos.Add(new SongInfo
				{
					Id = item.Id,
					Artist = item.Artist,
					Name = item.SongName,
					Ranking = likeRepository.GetSongRanking(item.Id)
				});
			}
			return songInfos.AsQueryable();
		}


		public IQueryable<Song> GetSongsByGenreId(int id)
		{
			IQueryable<Song> songs = DbContext.Songs.ToList().Where(x => x.GenreId == id).AsQueryable();
			if (songs != null)
			{
				return songs;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public void UpdateSong(int id, Song Entity)
		{
			var song = DbContext.Songs.FirstOrDefault(x => x.Id == id);
			if(song != null)
			{
				song.Artist = Entity.Artist;
				song.SongName = Entity.SongName;
				song.Album = Entity.Album;
				song.ReleaseDate = Entity.ReleaseDate;
				DbContext.SaveChanges();
			}
			else
			{
				throw new ArgumentException();
			}
		}
	}
}
