using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Requests;

namespace OpenAccount.Api.Controllers.Accounts
{
	/// <summary>
	/// وضعیت کیف پول
	/// </summary>
	public sealed class WalletStatusController : OpenAccountChainedController<WalletStatus, IWalletStatusBl, Guid>
	{
		public WalletStatusController(IConfiguration configuration,
			IHttpContextAccessor accessor,
			IWalletStatusBl baseLogic) :
			base(configuration, accessor, baseLogic, RequestStateType.WalletStatus)
		{
		}

		/// <summary>
		/// آخرین وضعیت کیف پول مشتری را برمی گرداند
		/// </summary>
		/// <returns>WalletStatus</returns>
		[HttpGet("GetWalletStatus")]
		public async Task<IActionResult> GetWalletStatus() => Ok(await ControllerLogic.GetWalletStatusForTotalBalance());

	}
}