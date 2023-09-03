/*using InfrastructureCore.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.Entities.Accounts;

namespace OpenAccount.Api.Controllers.Accounts
{
	public sealed class AccountTypeSettingController : ApplicationController<AccountTypeSetting, IAccountTypeSettingBl, short>
	{
		public AccountTypeSettingController(IConfiguration configuration, IHttpContextAccessor accessor, IAccountTypeSettingBl baseLogic) : base(configuration, accessor, baseLogic)
		{
		}

		[ApiExplorerSettings(IgnoreApi = false)]
		public override Task<IActionResult> Post([FromBody] AccountTypeSetting entity) => base.Post(entity);

		/// <summary>
		/// تنظیمات فعال هر نوع حساب را برمی گرداند
		/// </summary>
		/// <returns>List<AccountTypeSetting></returns>
		[HttpGet("GetActiveSettings")]
		public async Task<IActionResult> GetActiveSettings() => await GetAction(async () =>
		{
			return await ControllerLogic.GetActiveSettings();
		});
	}
}*/