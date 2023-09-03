using OpenAccount.Entities.Infrastructure;

namespace OpenAccount.RepositoryInterface.Infrastructure.ChainOfResp
{
	public interface IBaseChainOfRespRepository<TEntity, in TKey, TEnum> : IBaseRepository<TEntity, TKey>
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
		where TEnum : Enum
	{
	}
}