using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.Bl.Infrastructure
{
	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TRepository"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public abstract class BaseLogic<TEntity, TRepository, TKey> : BaseRoLogic<TEntity, TRepository, TKey>, IBaseLogic<TEntity, TKey>
		  where TEntity : IBaseEntity<TKey>
		  where TRepository : IBaseRepository<TEntity, TKey>
		  where TKey : struct
	{
		/// <summary>
		/// <typeparamref name="TRepository"/>
		/// </summary>
		/// <param name="logicRepository"></param>
		/// <param name="accessor"></param>
		protected BaseLogic(TRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		public virtual async Task Delete(TKey id, bool save = true) => await LogicRepository.Delete(id, save);

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		public virtual async Task Post(TEntity entity, bool save = true) => await LogicRepository.Add(entity, save);

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="entities"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		public virtual async Task PostAll(IEnumerable<TEntity> entities, bool save = true) => await LogicRepository.AddRange(entities, save);

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		public virtual async Task Put(TEntity entity, bool save = true) => await LogicRepository.Update(entity, save);

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public virtual async Task PutAll(IEnumerable<TEntity> entities) => await LogicRepository.UpdateRange(entities);
	}
}