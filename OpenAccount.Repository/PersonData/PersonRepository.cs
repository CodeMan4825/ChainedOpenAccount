using OpenAccount.Entities.PersonData;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Repository.PersonData
{
	internal sealed class PersonRepository : BasePersonRepository<Person>, IPersonRepository
	{
		public PersonRepository(AppDbContext context) : base(context)
		{
		}
	}
}