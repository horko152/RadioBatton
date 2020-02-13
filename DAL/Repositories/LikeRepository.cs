using DAL.Entities;

namespace DAL.Repositories
{
	public class LikeRepository : GeneralRepository<Like>
	{
		public LikeRepository(RadioBattonDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}
	}
}
