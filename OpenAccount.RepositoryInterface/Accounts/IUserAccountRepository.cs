using OpenAccount.Entities.Accounts;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Accounts
{
	/// <summary>
	/// افتتاح حساب کاربر
	/// </summary>
	public interface IUserAccountRepository : IBaseRepository<UserAccount, Guid>
	{
	}
}