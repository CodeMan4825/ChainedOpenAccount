using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.Entities.Requests;

namespace OpenAccount.BlInterface.Publics.Wallets
{
	/// <summary>
	/// برداشت از کیف پول مشتری
	/// </summary>
	public interface IWithdrawalFromWalletBl : IBaseLogic<WithdrawalFromWallet, Guid>
	{
		/// <summary>
		/// آیا برداشت از کیف پول با نوع خاص انجام شده است؟
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="used">برداشت های استفاده شده = true</param>
		/// <param name="eventType">نوع تراکنش</param>
		Task<bool> ControlWithdrawal(Guid requestId, bool used, EventType eventType);

		/// <summary>
		/// برداشت انجام شده از کیف پول مورد استفاده قرار گرفت
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="eventType">نوع تراکنش</param>
		/// <param name="save">If true, calls SaveChanges()</param>
		/// <returns></returns>
		Task MakeUsedWithdrawal(Guid requestId, EventType eventType, bool save = true);

		/// <summary>
		/// برداشت از کیف پول
		/// </summary>
		/// <param name="chargeAmount">مبلغ جهت برداشت</param>
		/// <param name="eventType">بابت</param>
		/// <param name="requestId">شناسه ی درخواست</param>
		/// <returns></returns>
		Task Withdrawal(long chargeAmount, EventType eventType, Guid requestId);

		/// <summary>
		/// انتقال از کیف پول به حساب اصلی
		/// </summary>
		/// <param name="chargeAmount">مبلغ جهت برداشت</param>
		/// <param name="request">درخواست</param>
		/// <param name="accountNumber">شماره حساب</param>
		/// <returns></returns>
		Task WithdrawalTransferToAccount(long chargeAmount, Request request, string accountNumber);
	}
}