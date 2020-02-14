using DAL.Entities;
using System;
using System.Linq;

namespace DAL.Repositories
{
	public class SongRepository : GeneralRepository<Song>
	{
		public SongRepository(RadioBattonDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}

		//public IQueryable<Song> GetSongsByArtist(string artist)
		//{
		//	IQueryable<Song> songs = DbContext.Songs.ToList().Where(x => x.Artist == artist).AsQueryable();
		//	if(songs != null)
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

		public IQueryable<Song> GetSongsByGenreId(int id)
		{
			IQueryable<Song> songs = DbContext.Songs.ToList().Where(x => x.GenreId == id).AsQueryable();
			if(songs !=null)
			{
				return songs;
			}
			else
			{
				throw new ArgumentException();
			}
		}
	}
}
