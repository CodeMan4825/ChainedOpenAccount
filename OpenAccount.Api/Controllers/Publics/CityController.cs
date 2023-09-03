using Microsoft.AspNetCore.Mvc;
using OpenAccount.Api.Infrastructure;
using OpenAccount.BlInterface.Publics;
using OpenAccount.Entities.Publics;

namespace OpenAccount.Api.Controllers.Publics
{
	/// <summary>
	/// شهر
	/// </summary>
	public sealed class CityController : ApplicationRoController<City, ICityBl, int>
	{
		public CityController(IConfiguration configuration, IHttpContextAccessor accessor, ICityBl baseLogic) : base(configuration, accessor, baseLogic)
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
		/// شهرهای یک استان
		/// </summary>
		/// <param name="provinceId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		[HttpGet("GetByProvinceId/{provinceId}")]
		public async Task<IActionResult> GetByProvinceId(int provinceId) => await GetAction(async () =>
		{
			return await ControllerLogic.GetByProvinceId(provinceId);
		});
	}
}