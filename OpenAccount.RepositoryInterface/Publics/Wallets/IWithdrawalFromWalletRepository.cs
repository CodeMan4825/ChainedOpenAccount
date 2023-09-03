using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Publics.Wallets
{
	/// <summary>
	/// برداشت از کیف پول مشتری
	/// </summary>
	public interface IWithdrawalFromWalletRepository : IBaseRepository<WithdrawalFromWallet, Guid>
	{
	}
}