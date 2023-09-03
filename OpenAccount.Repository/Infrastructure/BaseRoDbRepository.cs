using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.RepositoryInterface.Infrastructure;
using System.Linq.Expressions;

namespace OpenAccount.Repository.Infrastructure
{
	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <typeparam name="TContext"></typeparam>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public abstract class BaseRoDbRepository<TContext, TEntity, TKey> : BaseRepository<TContext, TEntity, TKey>, IBaseRoRepository<TEntity, TKey>
		where TContext : DbContext
		where TEntity : BaseEntity<TKey>
		where TKey : struct
	{
		/// <summary>
		/// Table in the Db.
		/// </summary>
		protected DbSet<TEntity> Entities;

		/// <summary>
		/// <typeparamref name="TContext"/>
		/// </summary>
		/// <param name="context"></param>
		protected BaseRoDbRepository(TContext context) : base(context) => Entities = context.Set<TEntity>();

		/// <summary>
		/// <<typeparamref name="TEntity"/>
		/// </summary>
		/// <returns></returns>
		public virtual async Task<IEnumerable<TEntity>> Get() => await Entities.ToListAsync();

		/// <summary>
		/// <typeparamref name="TKey"/>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual async Task<TEntity?> Get(TKey id) => await FindById(id);

		/// <summary>
		/// <typeparamref name="TKey"/>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public async Task<IEnumerable<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> predicate) => await Entities.AsQueryable().Where(predicate).ToListAsync();

		/// <summary>
		/// <typeparamref name="TKey"/>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual async Task<TEntity?> FindById(TKey id) => await Entities.FindAsync(id);

		/// <summary>
		/// Paging result.
		/// </summary>
		/// <param name="page">Which page you want ?</param>
		/// <param name="orderby">Sort by which field ?</param>
		/// <param name="query">if you have entity with include or where, send it, else leave it null.</param>
		/// <param name="pageSize">Count of rows in each page.</param>
		/// <returns></returns>
		public async Task<IEnumerable<TEntity>> Pagination<TOKey, TOEntity>(int page,
			Expression<Func<TEntity, TOKey>> @orderby, IIncludableQueryable<TEntity, TOEntity> query, int pageSize = 10) =>
			await query.OrderBy(@orderby).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

		/// <summary>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <returns></returns>
		public IQueryable<TEntity> AsQuery() => Entities.AsNoTracking().AsQueryable();
	}
}