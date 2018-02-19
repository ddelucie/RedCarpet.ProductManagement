using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RedCarpet.Data
{


	public class DataRepository : IDataRepository
	{

		public async Task<TEntity> Find<TEntity>(int id) where TEntity : class
		{
			using (var context = new RedCarpetDBContext())
			{
				return await context.Set<TEntity>().FindAsync(new object[] { id });
			}
		}


		public virtual async Task<TEntity> GetFirstAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null)
			where TEntity : class 
		{
			using (var context = new RedCarpetDBContext())
			{
				IQueryable<TEntity> query = context.Set<TEntity>();

				if (filter != null)
				{
					return await query.Where(filter).AsNoTracking().FirstOrDefaultAsync();
				}

				return null;
			}
		}

		public void Create<TEntity>(TEntity entity) where TEntity : class
		{
			using (var context = new RedCarpetDBContext())
			{
				context.Set<TEntity>().Add(entity);
				context.SaveChanges();
			}
		}

		public void CreateList<TEntity>(IList<TEntity> entities) where TEntity : class
		{
			using (var context = new RedCarpetDBContext())
			{
				foreach (var entity in entities)
				{
					context.SaveChanges();
					context.Set<TEntity>().Add(entity);
				}
				context.SaveChanges();
			}
		}

		public void Update<TEntity>(TEntity entity) where TEntity : class
		{
			using (var context = new RedCarpetDBContext())
			{
				context.Set<TEntity>().Attach(entity);
				context.Entry(entity).State = EntityState.Modified;
				context.SaveChanges();
			}
		}


		public void UpdateList<TEntity>(IList<TEntity> entities) where TEntity : class
		{
			using (var context = new RedCarpetDBContext())
			{
				foreach (var entity in entities)
				{
					context.Set<TEntity>().Attach(entity);
					context.Entry(entity).State = EntityState.Modified;
				}

				context.SaveChanges();
			}
		}

	}
}