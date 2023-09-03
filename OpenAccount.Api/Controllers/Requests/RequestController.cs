using Microsoft.AspNetCore.Mvc;
using OpenAccount.Api.Infrastructure;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers.Requests
{
	public sealed class RequestController : ApplicationRoController<Request, IRequestBl, Guid>
	{
		public RequestController(IConfiguration configuration, IHttpContextAccessor accessor, IRequestBl baseLogic) : base(configuration, accessor, baseLogic)
		{
		}


		/// <summary>
		/// Get requests by userId.
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAvailableRequests")]
		public async Task<IActionResult> GetAvalableRequests() => Ok(await ControllerLogic.GetAvalableRequests());

		//[HttpGet("Exception")]
		//public Task<IActionResult> Exception() =>
		//	throw StException.InquiryNotAcceptable();

		//[HttpGet("ExceptionDto")]
		//public Task<IActionResult> ExceptionDto() =>
		//	throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(RequestStateType.FacilityInquery, "salam"));
	}
}