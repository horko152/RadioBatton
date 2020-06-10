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
			if (likes != null)
			{
				return likes;
			}
			else
			{
				throw new ArgumentException();
			}
		}
		public int GetSongRanking(int id)
		{
			var likes = GetLikesBySongId(id);
			int countOfLikes = 0;
			int countOfDislikes = 0;
			int allLikes = likes.Count();
			double ranking = 0;
			foreach (var item in likes)
			{
				if (item.LikeValue == true)
				{
					countOfLikes++;
				}
				else
				{
					countOfDislikes++;
				}
			}
			if (countOfLikes != countOfDislikes)
			{
				ranking = (countOfLikes / allLikes) * 100;
			}
			else if (countOfLikes == countOfDislikes && (countOfLikes != 0 || countOfDislikes != 0))
			{
				ranking = 50;
			}
			else
			{
				ranking = 0;
			}
			return Convert.ToInt32(ranking);
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
