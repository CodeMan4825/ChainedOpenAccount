using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.BlInterface.Publics.Wallets;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Publics.Wallets;

namespace OpenAccount.Bl.Accounts
{
	/// <summary>
	/// انتقال از کیف پول به حساب اصلی
	/// </summary>
	internal sealed class WithdrawalTransferToAccountBl : OpenAccountChainedBl<WithdrawalFromWallet, IWithdrawalFromWalletRepository, Guid>, IWithdrawalTransferToAccountBl
	{
		private readonly IRequestBl RequestBl;
		private readonly IWithdrawalFromWalletBl WithdrawalBl;
		private readonly IUserAccountBl UserAccountBl;

		public WithdrawalTransferToAccountBl(IWithdrawalFromWalletRepository logicRepository, 
			ISendUserAccountIBanToBankBl preRequest,
			IHttpContextAccessor accessor,
			IRequestBl requestBl,
			IUserAccountBl userAccountBl,
			IWithdrawalFromWalletBl withdrawalBl,
			IRequestStateLogBl requestLog) : base(logicRepository, preRequest, RequestStateType.TransferFeeToAccount, accessor, requestLog)
		{
			RequestBl = requestBl;
			UserAccountBl = userAccountBl;
			WithdrawalBl = withdrawalBl;
		}

		public override RequestStateType GetNextStep() => RequestStateType.Finished;

		/// <summary>
		/// انتقال از کیف پول به حساب اصلی
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public async Task TransferToAccount(Guid requestId)
		{
			var request = await RequestBl.Get(requestId) ?? throw StException.RequestIdNotFound();
			var setting = await RequestBl.GetAccountTypeSetting(requestId) ?? throw StException.DataNotFound("خطا در بارگذاری تنظیمات حساب");
			var account = await UserAccountBl.Get(requestId) ?? throw StException.DataNotFound("افتتاح حسابی هنوز انجام نشده است ");

			await GoToNextStep(request);
			// انتقال از کیف پول به حساب اصلی
			await WithdrawalBl.WithdrawalTransferToAccount(setting.MinBalance, request, account.AccountNumber);
		}

		public override void Validate()
		{
		}
	}
}