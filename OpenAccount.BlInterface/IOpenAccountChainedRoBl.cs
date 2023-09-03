using OpenAccount.BlInterface.Infrastructure.ChainOfResp;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.Requests;

namespace OpenAccount.BlInterface
{
	/// <summary>
	/// <inheritdoc/>
	/// With RequestStateType as TEnum.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public interface IOpenAccountChainedRoBl<TEntity, TKey> : IBaseChainOfRespRoLogic<TEntity, TKey, RequestStateType>
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
	{
	}
}