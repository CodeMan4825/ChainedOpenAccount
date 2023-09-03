using OpenAccount.Entities.Accounts;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Accounts
{
	/// <summary>
	/// وضعیت کیف پول
	/// </summary>
	public interface IWalletStatusRepository : IBaseRepository<WalletStatus, Guid>
	{
	}
}