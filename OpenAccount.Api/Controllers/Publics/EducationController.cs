using Microsoft.AspNetCore.Mvc;
using OpenAccount.Api.Infrastructure;
using OpenAccount.BlInterface.Publics;
using OpenAccount.Entities.Publics;

namespace OpenAccount.Api.Controllers.Publics
{
	/// <summary>
	/// تحصیلات
	/// </summary>
	public sealed class EducationController : ApplicationRoController<Education, IEducationBl, byte>
	{
		public EducationController(IConfiguration configuration, IHttpContextAccessor accessor, IEducationBl baseLogic, bool ThrowUserData = true) : base(configuration, accessor, baseLogic, ThrowUserData)
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
		public override Task<IActionResult> Get(byte id) => base.Get(id);
	}
}