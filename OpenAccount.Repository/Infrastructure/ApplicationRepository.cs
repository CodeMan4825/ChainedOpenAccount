using OpenAccount.Entities.Infrastructure;

namespace OpenAccount.Repository.Infrastructure
{
	internal abstract class ApplicationRepository<TEntity, TKey> : BaseDbRepository<AppDbContext, TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : struct
	{
		protected ApplicationRepository(AppDbContext context) : base(context) => Context = context;
	}
}