using OpenAccount.Entities.Infrastructure;

namespace OpenAccount.RepositoryInterface.Infrastructure
{
	/// <summary>
	/// Repository
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public interface IBaseRepository<TEntity, in TKey> : IBaseRoRepository<TEntity, TKey>
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
	{
		/// <summary>
		/// Post all entities to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entities"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		Task AddRange(IEnumerable<TEntity> entities, bool save = true);

		/// <summary>
		/// Post entity to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="save">If true, calls SaveChanges()</param>
		/// <returns></returns>
		Task Add(TEntity entity, bool save = true);

		/// <summary>
		/// Update all entites to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entities"></param>
		/// <param name="save">If true, calls SaveChanges()</param>
		/// <returns></returns>
		Task UpdateRange(IEnumerable<TEntity> entities, bool save = true);

		/// <summary>
		/// Update entity to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="save">If true, calls SaveChanges()</param>
		/// <returns></returns>
		Task Update(TEntity entity, bool save = true);

		/// <summary>
		/// Delete entity by Id.
		/// <typeparamref name="TKey"/>
		/// </summary>
		/// <param name="id">Find entity by Id</param>
		/// <param name="save">If true, calls SaveChanges()</param>
		/// <returns></returns>
		Task Delete(TKey id, bool save = true);
	}
}