using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Publics.Exceptions
{
	public interface IEntityExceptionRepository : IBaseExceptionRepository<EntityException>
    {
    }

    public interface IBaseExceptionRepository<TEntity> : IBaseRepository<TEntity, Guid>
		where TEntity : EntityException
	{

	}
}