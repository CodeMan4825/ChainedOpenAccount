using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Publics.Exceptions;

namespace OpenAccount.Repository.Publics.Exceptions
{
    internal sealed class StartExceptionRepository : BaseExceptionRepository<StartException>, IStartExceptionRepository
    {
        public StartExceptionRepository(AppDbContext context) : base(context)
        {
        }
    }
}