using DAL.Entities;

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
	}
}
