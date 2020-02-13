using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DAL.Repositories
{
	public abstract class GeneralRepository<T> : IGeneralRepository<T> where T : class
	{
		protected RadioBattonDbContext DbContext { get; set; }
		public GeneralRepository(RadioBattonDbContext DbContext)
		{
			this.DbContext = DbContext;
		}
		public IQueryable<T> GetAll()
		{
			return this.DbContext.Set<T>().AsNoTracking();
		}

		public T GetById(int id)
		{
			T entity = DbContext.Find<T>(id);
			if (entity != null)
			{
				return entity;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public void Create(T entity)
		{
			DbContext.Add(entity);
			DbContext.SaveChanges();
		}

		public void Update(int id, T entity)
		{
			if (DbContext.Find<T>(id) != null)
			{
				DbContext.Entry(entity).State = EntityState.Modified;
				DbContext.SaveChanges();
			}
		}

		public void Delete(int id)
		{
			T entity = DbContext.Find<T>(id);
			if (entity != null)
			{
				DbContext.Remove(entity);
				DbContext.SaveChanges();
			}
			else
			{
				throw new ArgumentException();
			}
		}
	}
}
