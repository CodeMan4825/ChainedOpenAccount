using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.Wallets;

namespace OpenAccount.BlInterface.Accounts
{
	/// <summary>
	/// وضعیت کیف پول
	/// </summary>
	public interface IWalletStatusBl : IOpenAccountChainedBl<WalletStatus, Guid>
	{
		/// <summary>
		/// آخرین وضعیت کیف پول مشتری را برای کل هزینه ها برمی گرداند
		/// </summary>
		/// <returns>WalletStatusResponseBriefDto</returns>
		Task<WalletStatusResponseBriefDto> GetWalletStatusForTotalBalance();

		/// <summary>
		/// موجودی کیف پول مشتری را برمی گرداند
		/// </summary>
		/// <returns></returns>
		Task<HttpSimorghApiResponseDto<WalletStatusResponseDto>> GetWalletStatus();

		/// <summary>
		/// آخرین وضعیت کیف پول مشتری لاگین کرده را برای نوع تراکنش برمی گرداند
		/// </summary>
		/// <param name="eventType"></param>
		/// <returns></returns>
		Task<WalletStatusResponseBriefDto> GetWalletStatusForType(EventType eventType);
	}
}