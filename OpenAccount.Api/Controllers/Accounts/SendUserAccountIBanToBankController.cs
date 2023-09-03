using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Requests;

namespace OpenAccount.Api.Controllers.Accounts
{
	/// <summary>
	/// ارسال شبای حساب به بانک
	/// </summary>
	public sealed class SendUserAccountIBanToBankController : OpenAccountChainedController<UserAccount, ISendUserAccountIBanToBankBl, Guid>
	{
		public SendUserAccountIBanToBankController(IConfiguration configuration, IHttpContextAccessor accessor, ISendUserAccountIBanToBankBl baseLogic) : base(configuration, accessor, baseLogic, RequestStateType.SendIBanToBank)
		{
		}

		/// <summary>
		/// ارسال شبای حساب به بانک
		/// </summary>
		/// <returns></returns>
		[HttpPost("SentIBanToBank")]
		public async Task SentIBanToBank() => await ControllerLogic.SendIBanToBank(RequestId);
	}
}