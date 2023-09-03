using Microsoft.AspNetCore.Mvc;
using OpenAccount.Api.Infrastructure;
using OpenAccount.BlInterface.Publics;
using OpenAccount.Entities.Publics;

namespace OpenAccount.Api.Controllers.Publics
{
	/// <summary>
	/// استان
	/// </summary>
	public sealed class ProvinceController : ApplicationRoController<Province, IProvinceBl, int>
	{
		public ProvinceController(IConfiguration configuration, IHttpContextAccessor accessor, IProvinceBl baseLogic) : base(configuration, accessor, baseLogic)
		{
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = false)]
		public override Task<IActionResult> Get() => base.Get();

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ApiExplorerSettings(IgnoreApi = false)]
		public override Task<IActionResult> Get(int id) => base.Get(id);
	}
}