using Microsoft.AspNetCore.Mvc;
using OpenAccount.Api.Infrastructure;
using OpenAccount.BlInterface.Publics;
using OpenAccount.Entities.Publics;

namespace OpenAccount.Api.Controllers.Publics
{
	/// <summary>
	/// گروه مشاغل
	/// </summary>
	public sealed class JobCategoryController : ApplicationController<JobCategory, IJobCategoryBl, byte>
	{
		public JobCategoryController(IConfiguration configuration, IHttpContextAccessor accessor, IJobCategoryBl baseLogic, bool ThrowUserData = true) : base(configuration, accessor, baseLogic, ThrowUserData)
		{
		}

		/// <summary>
		/// مشاغل فعال را برگردان
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetActives")]
		public async Task<IActionResult> GetActives() => await GetAction(async () =>
		{
			return await ControllerLogic.GetActives();
		});

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = false)]
		public override Task<IActionResult> Get(byte id) => base.Get(id);
	}
}