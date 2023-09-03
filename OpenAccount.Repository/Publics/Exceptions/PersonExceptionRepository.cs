using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Publics.Exceptions;

namespace OpenAccount.Repository.Publics.Exceptions
{
	internal sealed class PersonExceptionRepository : BaseExceptionRepository<PersonException>, IPersonExceptionRepository
	{
		public PersonExceptionRepository(AppDbContext context) : base(context)
		{
		}
	}
}