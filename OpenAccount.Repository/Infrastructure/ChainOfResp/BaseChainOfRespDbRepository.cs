using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.RepositoryInterface.Infrastructure.ChainOfResp;

namespace OpenAccount.Repository.Infrastructure.ChainOfResp
{
	public abstract class BaseChainOfRespDbRepository<TContext, TEntity, TKey, TEnum> : BaseDbRepository<TContext, TEntity, TKey>, IBaseChainOfRespRepository<TEntity, TKey, TEnum>
		where TContext : DbContext
		where TEntity : BaseEntity<TKey>
		where TKey : struct
		where TEnum : Enum
	{
		protected BaseChainOfRespDbRepository(TContext context) : base(context)
		{
		}
	}
}