using OpenAccount.Entities.Infrastructure;

namespace OpenAccount.BlInterface.Infrastructure
{
	/// <summary>
	/// Business logic
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public interface IBaseLogic<TEntity, in TKey> : IBaseRoLogic<TEntity, TKey>
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
	{
		/// <summary>
		/// Post entity to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="save">If true, calls SaveChanges()</param>
		/// <returns></returns>
		Task Post(TEntity entity, bool save = true);

		/// <summary>
		/// Post all entities to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entities"></param>
		/// <param name="save">If true, calls SaveChanges()</param>
		/// <returns></returns>
		Task PostAll(IEnumerable<TEntity> entities, bool save = true);

		/// <summary>
		/// Update entity to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="save">If true, calls SaveChanges()</param>
		/// <returns></returns>
		Task Put(TEntity entity, bool save = true);

		/// <summary>
		/// Update all entites to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		Task PutAll(IEnumerable<TEntity> entities);

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