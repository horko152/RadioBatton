using DAL.Entities;

namespace DAL.Repositories
{
	public class SongRepository : GeneralRepository<Song>
	{
		public SongRepository(RadioBattonDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}
	}
}
