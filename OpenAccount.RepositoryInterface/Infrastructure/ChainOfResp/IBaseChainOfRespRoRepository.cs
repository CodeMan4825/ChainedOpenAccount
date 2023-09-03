using OpenAccount.Entities.Infrastructure;

namespace OpenAccount.RepositoryInterface.Infrastructure.ChainOfResp
{
	public interface IBaseChainOfRespRoRepository<TEntity, in TKey, TEnum> : IBaseRoRepository<TEntity, TKey>
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
		where TEnum : Enum
	{
	}
}