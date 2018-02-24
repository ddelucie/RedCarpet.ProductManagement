using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RedCarpet.Data
{
	public interface IDataRepository
	{
		Task<T> Find<T>(int id) where T : class;
		IList<TEntity> GetAll<TEntity>() where TEntity : class;
 
		void Create<T>(T entity) where T : class;
		void CreateList<TEntity>(IList<TEntity> entities) where TEntity : class;
		void Update<T>(T entity) where T : class;
		void UpdateList<T>(IList<T> entity) where T : class;

		Task<TEntity> GetFirstAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)	where TEntity : class;

	}
}
