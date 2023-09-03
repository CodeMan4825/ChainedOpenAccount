using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers.Requests
{
	/// <summary>
	/// سفارش کارت
	/// </summary>
	public sealed class RequestCardController : OpenAccountChainedController<RequestCard, IRequestCardBl, Guid>
	{
		public RequestCardController(IConfiguration configuration, IHttpContextAccessor accessor, IRequestCardBl baseLogic) : 
			base(configuration, accessor, baseLogic, RequestStateType.CardOrder)
		{
		}

		/// <summary>
		/// لیست کارت های موجود را از سرویس کارت برگردان
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetCardsFromService")]
		public async Task<IEnumerable<RequestCard>> GetCardsFromService() => await ControllerLogic.GetCardsFromService();

		[ApiExplorerSettings(IgnoreApi = false)]
		public override async Task<IActionResult> Post([FromBody] RequestCard entity)
		{
			if (!entity.IsActive)
				throw StException.IncorrectData("کارت در حالت غیرفعال پذیرفته نمی باشد");

			return await base.Post(entity);
		}

		[ApiExplorerSettings(IgnoreApi = false)]
		public override async Task<IActionResult> Put([FromBody] RequestCard entity)
		{
			if (!entity.IsActive)
				throw StException.IncorrectData("کارت در حالت غیرفعال پذیرفته نمی باشد");

			return await base.Put(entity);
		}
	}
}