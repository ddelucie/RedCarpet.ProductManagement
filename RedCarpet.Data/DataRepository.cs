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

		public async Task<TEntity> Find<TEntity>(string id) where TEntity : class
		{
			using (var context = new RedCarpetDBContext())
			{
				return await context.Set<TEntity>().FindAsync(new object[] { id });
			}
		}

		protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
				Expression<Func<TEntity, bool>> filter = null,
				Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
				string includeProperties = null,
				int? skip = null,
				int? take = null)
				where TEntity : class
		{

			using (var context = new RedCarpetDBContext())
			{
				IQueryable<TEntity> query = context.Set<TEntity>();

				if (filter != null)
				{
					query = query.Where(filter);
				}

				foreach (var includeProperty in includeProperties.Split
					(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}

				if (orderBy != null)
				{
					query = orderBy(query);
				}

				if (skip.HasValue)
				{
					query = query.Skip(skip.Value);
				}

				if (take.HasValue)
				{
					query = query.Take(take.Value);
				}

				return query;
			}
		}

		public virtual async Task<TEntity> GetFirstAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null)
			where TEntity : class 
		{
			return await GetQueryable<TEntity>(filter).FirstOrDefaultAsync();
		}

		public void Create<TEntity>(TEntity entity) where TEntity : class
		{
			using (var context = new RedCarpetDBContext())
			{
				context.Set<TEntity>().Add(entity);
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
	}
}