using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.RepositoryInterface.Infrastructure.ChainOfResp;

namespace OpenAccount.Repository.Infrastructure.ChainOfResp
{
	public abstract class BaseChainOfRespRoDbRepository<TContext, TEntity, TKey, TEnum> : BaseRoDbRepository<TContext, TEntity, TKey>, IBaseChainOfRespRoRepository<TEntity, TKey, TEnum>
		where TContext : DbContext
		where TEntity : BaseEntity<TKey>
		where TKey : struct
		where TEnum : Enum
	{
		protected BaseChainOfRespRoDbRepository(TContext context) : base(context)
		{
		}
	}
}