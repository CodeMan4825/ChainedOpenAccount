using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Publics.Exceptions;

namespace OpenAccount.Repository.Publics.Exceptions
{
	internal sealed class EntityExceptionRepository : BaseExceptionRepository<EntityException>, IEntityExceptionRepository
    {
        public EntityExceptionRepository(AppDbContext context) : base(context)
        {
        }
    }

	internal class BaseExceptionRepository<TEntity> : ApplicationRepository<TEntity, Guid>
		where TEntity : EntityException
	{
		public BaseExceptionRepository(AppDbContext context) : base(context)
		{
		}
	}
}