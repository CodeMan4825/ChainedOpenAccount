using OpenAccount.Entities.PersonData;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Repository.PersonData
{
	internal abstract class BasePersonRepository<TEntity> : ApplicationRepository<TEntity, Guid>, IBasePersonRepository<TEntity>
		where TEntity : Person
	{
		protected BasePersonRepository(AppDbContext context) : base(context)
		{
		}
	}
}