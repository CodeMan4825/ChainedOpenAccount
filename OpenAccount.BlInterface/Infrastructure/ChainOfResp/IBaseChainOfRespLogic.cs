using OpenAccount.Entities.Infrastructure;

namespace OpenAccount.BlInterface.Infrastructure.ChainOfResp
{
	/// <summary>
	/// Base of ChainOfResp for crud bl.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TEnum"></typeparam>
	public interface IBaseChainOfRespLogic<TEntity, in TKey, TEnum> : IBaseChainOfRespRoLogic<TEntity, TKey, TEnum>, IBaseLogic<TEntity, TKey>
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
		where TEnum : Enum
	{
	}
}