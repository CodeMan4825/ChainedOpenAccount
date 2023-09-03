using OpenAccount.Entities.Infrastructure;

namespace OpenAccount.Repository.Infrastructure
{
	public abstract class ApplicationRoRepository<TEntity, TKey> : BaseRoDbRepository<AppDbContext, TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : struct
	{
		protected ApplicationRoRepository(AppDbContext context) : base(context)
		{
		}
	}
}