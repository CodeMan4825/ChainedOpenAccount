using Microsoft.EntityFrameworkCore.Query;
using OpenAccount.Entities.Infrastructure;
using System.Linq.Expressions;

namespace OpenAccount.RepositoryInterface.Infrastructure
{
	/// <summary>
	/// Read Only repository.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public interface IBaseRoRepository<TEntity, in TKey> : IDisposable
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
	{
		/// <summary>
		/// Gets all entities.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<TEntity>> Get();

		/// <summary>
		/// Find 1 entity by Id.
		/// <typeparamref name="TKey"/>
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<TEntity?> Get(TKey id);

		/// <summary>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="predicate">Where clause</param>
		/// <returns></returns>
		Task<IEnumerable<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// <typeparamref name="TKey"/>
		/// </summary>
		/// <param name="id">Id to find entity.</param>
		/// <returns></returns>
		Task<TEntity?> FindById(TKey id);

		/// <summary>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TOKey"></typeparam>
		/// <typeparam name="TOEntity"></typeparam>
		/// <param name="page">Page number</param>
		/// <param name="orderby">Order data by...</param>
		/// <param name="query">Query to pagination</param>
		/// <param name="pageSize">Size of each page.</param>
		/// <returns>One page of data.</returns>
		Task<IEnumerable<TEntity>> Pagination<TOKey, TOEntity>(int page, Expression<Func<TEntity, TOKey>> orderby, IIncludableQueryable<TEntity, TOEntity> query, int pageSize = 10);

		/// <summary>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <returns>Get list of data to filter in Business logic</returns>
		IQueryable<TEntity> AsQuery();
	}
}