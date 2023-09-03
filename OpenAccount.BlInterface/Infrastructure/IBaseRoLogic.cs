using OpenAccount.Entities.Infrastructure;
using OpenAccount.Publics;

namespace OpenAccount.BlInterface.Infrastructure
{
	/// <summary>
	/// ReadOnly business logic.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public interface IBaseRoLogic<TEntity, in TKey>
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
	{
		/// <summary>
		/// Gets all entities.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <returns>All entities</returns>
		Task<IEnumerable<TEntity>> Get();

		/// <summary>
		/// Find 1 entity by Id.
		/// <typeparamref name="TEntity"/>
		/// <typeparamref name="TKey"/>
		/// </summary>
		/// <param name="id">Find data by Id</param>
		/// <returns>Founded entity.</returns>
		Task<TEntity?> Get(TKey id);

		/// <summary>
		/// Add user props into dictionary.
		/// </summary>
		/// <returns>Dictionary str, str</returns>
		Dictionary<string, string> GetUserDataFromHeaderAsDictionary();

		/// <summary>
		/// Do u want to know your real exception before catch ?
		/// Override this method and watch it.
		/// </summary>
		/// <param name="result"></param>
		/// <param name="exception"></param>
		/// <returns></returns>
		Task HandledExceptions(HttpStResult? result, Exception? exception);
	}
}