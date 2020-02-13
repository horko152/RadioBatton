using DAL.Entities;

namespace DAL.Repositories
{
	public class GenreRepository : GeneralRepository<Genre>
	{
		public GenreRepository(RadioBattonDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}
	}
}
