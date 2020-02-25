using DAL.Entities;
using System;
using System.Linq;

namespace DAL.Repositories
{
	public class GenreRepository : GeneralRepository<Genre>
	{
		public GenreRepository(RadioBattonDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}

		public void CreateGenre(Genre Entity)
		{
			DbContext.Add(Entity);
			DbContext.SaveChanges();
		}
		public void UpdateGenre(int id, Genre Entity)
		{
			var genre = DbContext.Genres.FirstOrDefault(x => x.Id == id);
			if (genre != null)
			{
				genre.GenreName = Entity.GenreName;
				genre.Description = Entity.Description;
				DbContext.SaveChanges();
			}
			else
			{
				throw new ArgumentException();
			}
		}
	}
}
