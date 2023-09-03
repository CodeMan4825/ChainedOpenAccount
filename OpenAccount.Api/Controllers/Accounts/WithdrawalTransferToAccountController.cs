using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers.Accounts
{
	/// <summary>
	/// انتقال از کیف پول به حساب اصلی
	/// </summary>
	public sealed class WithdrawalTransferToAccountController : OpenAccountChainedController<WithdrawalFromWallet, IWithdrawalTransferToAccountBl, Guid>
	{
		public WithdrawalTransferToAccountController(IConfiguration configuration, IHttpContextAccessor accessor, IWithdrawalTransferToAccountBl baseLogic) : base(configuration, accessor, baseLogic, RequestStateType.TransferFeeToAccount)
		{
		}

		/// <summary>
		/// انتقال از کیف پول به حساب اصلی
		/// </summary>
		/// <returns></returns>
		[HttpPost("TransferToAccount")]
		public async Task TransferToAccount()
		{
			if (!RequestIdExists())
				throw StException.RequestIdNotFound();

			await ControllerLogic.TransferToAccount(RequestId);
		}
	}
}