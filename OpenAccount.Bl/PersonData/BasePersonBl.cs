using Microsoft.AspNetCore.Http;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.Entities.PersonData;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Bl.PersonData
{
	internal abstract class BasePersonBl<TEntity, TRepository> : BaseLogic<TEntity, TRepository, Guid>, IBasePersonBl<TEntity>
		where TEntity : Person
		where TRepository : IBasePersonRepository<TEntity>
	{
		protected BasePersonBl(TRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}
	}
}