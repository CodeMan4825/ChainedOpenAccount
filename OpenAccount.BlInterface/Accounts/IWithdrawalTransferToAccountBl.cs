using OpenAccount.Entities.Publics.Wallets;

namespace OpenAccount.BlInterface.Accounts
{
	/// <summary>
	/// انتقال از کیف پول به حساب اصلی
	/// </summary>
	public interface IWithdrawalTransferToAccountBl : IOpenAccountChainedBl<WithdrawalFromWallet, Guid>
	{
		/// <summary>
		/// انتقال از کیف پول به حساب اصلی
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task TransferToAccount(Guid requestId);
	}
}