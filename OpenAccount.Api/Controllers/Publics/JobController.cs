using Microsoft.AspNetCore.Mvc;
using OpenAccount.Api.Infrastructure;
using OpenAccount.BlInterface.Publics;
using OpenAccount.Entities.Publics;

namespace OpenAccount.Api.Controllers.Publics
{
	/// <summary>
	/// مشاغل
	/// </summary>
	public sealed class JobController : ApplicationController<Job, IJobBl, int>
	{
		public JobController(IConfiguration configuration, IHttpContextAccessor accessor, IJobBl baseLogic) : base(configuration, accessor, baseLogic)
		{
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = false)]
		public override Task<IActionResult> Get(int id) => base.Get(id);

		/// <summary>
		/// لیست مشاغل فعال یک گروه شغلی
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		[HttpGet("GetJobsByCategory")]
		public async Task<IActionResult> GetJobsByCategory(byte categoryId) => await GetAction(async () =>
		{
			return await ControllerLogic.GetJobsByCategory(categoryId);
		});
	}
}