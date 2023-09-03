using OpenAccount.Entities.PersonData;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Repository.PersonData
{
	internal sealed class LegalPersonRepository : BasePersonRepository<LegalPerson>, ILegalPersonRepository
	{
		public LegalPersonRepository(AppDbContext context) : base(context)
		{
		}
	}
}