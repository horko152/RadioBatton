using DAL.Entities;

namespace DAL.Repositories
{
	public class UserRepository : GeneralRepository<User>
	{
		public UserRepository(RadioBattonDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}
	}
}
