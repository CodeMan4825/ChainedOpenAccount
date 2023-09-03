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
	public interface IOpenAccountChainedBl<TEntity, TKey> : IBaseChainOfRespLogic<TEntity, TKey, RequestStateType>
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
	{
	}
}