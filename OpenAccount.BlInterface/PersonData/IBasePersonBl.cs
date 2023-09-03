using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.PersonData;

namespace OpenAccount.BlInterface.PersonData
{
	public interface IBasePersonBl<TEntity> : IBaseLogic<TEntity, Guid>
		where TEntity : Person
	{
	}
}