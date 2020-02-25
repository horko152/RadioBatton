using DAL.Entities;
using System;
using System.Linq;

namespace DAL.Repositories
{
	public class LikeRepository : GeneralRepository<Like>
	{
		public LikeRepository(RadioBattonDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}
		public void CreateLike(Like Entity)
		{
			DbContext.Add(Entity);
			DbContext.SaveChanges();
		}

		public IQueryable<Like> GetLikesBySongId(int id)
		{
			IQueryable<Like> likes = DbContext.Likes.ToList().Where(x => x.SongId == id).AsQueryable();
			if(likes != null)
			{
				return likes;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public void UpdateLike(int id, Like Entity)
		{
			var like = DbContext.Likes.FirstOrDefault(x => x.Id == id);
			if (like != null)
			{
				like.LikeValue = Entity.LikeValue;
				DbContext.SaveChanges();
			}
			else
			{
				throw new ArgumentException();
			}
		}
	}
}
