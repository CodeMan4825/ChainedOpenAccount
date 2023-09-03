using OpenAccount.Entities.PersonData;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.PersonData
{
	public interface IBasePersonRepository<TEntity> : IBaseRepository<TEntity, Guid>
		where TEntity : Person
	{
	}
}