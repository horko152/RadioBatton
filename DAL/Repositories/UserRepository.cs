using DAL.Entities;
using System;
using System.Linq;

namespace DAL.Repositories
{
	public class UserRepository : GeneralRepository<User>
	{
		public UserRepository(RadioBattonDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}
		public User GetByUsername(string username)
		{
			User user = DbContext.Users.FirstOrDefault(x => x.UserName == username);
			//User user = DbContext.Users.Where(u => u.UserName.Equals(username)).First();
			return user;
		}
		public User GetByEmail(string email)
		{
			User user = DbContext.Users.FirstOrDefault(x => x.Email == email);
			return user;
		}

		public void CreateUser(User Entity)
		{
			DbContext.Add(Entity);
			DbContext.SaveChanges();
		}

		public void UpdateUser(int id, User Entity)
		{
			var user = DbContext.Users.FirstOrDefault(x => x.Id == id);
			if(user != null)
			{
				user.Email = Entity.Email;
				user.UserName = Entity.UserName;
				user.Password = Entity.Password;
				DbContext.SaveChanges();
			}
		}

	}
}
