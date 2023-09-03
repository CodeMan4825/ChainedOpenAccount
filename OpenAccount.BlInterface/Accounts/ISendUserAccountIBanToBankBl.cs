using OpenAccount.Entities.Accounts;

namespace OpenAccount.BlInterface.Accounts
{
	/// <summary>
	/// ارسال شبای حساب به بانک
	/// </summary>
	public interface ISendUserAccountIBanToBankBl : IOpenAccountChainedBl<UserAccount, Guid>
	{
		/// <summary>
		/// ارسال شبای حساب به بانک
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task SendIBanToBank(Guid requestId);
	}
}