using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Requests;

namespace OpenAccount.Api.Controllers.Accounts
{
	/// <summary>
	/// افتتاح حساب کاربر
	/// </summary>
	public sealed class UserAccountController : OpenAccountChainedController<UserAccount, IUserAccountBl, Guid>
	{
		public UserAccountController(IConfiguration configuration, IHttpContextAccessor accessor, IUserAccountBl baseLogic) : 
			base(configuration, accessor, baseLogic, RequestStateType.UserAccount)
		{
		}

		[HttpPost("OpenAccount")]
		public async Task<IActionResult> OpenAccount() => Ok(await ControllerLogic.OpenAccount(RequestId));
	}
}