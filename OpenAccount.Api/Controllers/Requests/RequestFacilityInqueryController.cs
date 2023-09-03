using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Requests;
using OpenAccount.Entities.Requests.InqueryLoan;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers.Requests
{
	/// <summary>
	/// استعلام تسهیلات
	/// </summary>
	public sealed class RequestFacilityInqueryController : OpenAccountChainedController<SamatLoanInquiryRequest, IRequestFacilityInqueryBl, Guid>
	{
		public RequestFacilityInqueryController(IConfiguration configuration, IHttpContextAccessor accessor, IRequestFacilityInqueryBl baseLogic) : 
			base(configuration, accessor, baseLogic, RequestStateType.FacilityInquery)
		{
		}

	/*	public override async Task HandledExceptions([FromBody] HandleExceptionParam param) => await ControllerLogic.InqueryForRealPeron(new()
		{
			ActionMessage = $"{param.Result?.Value?.ToString()} : {param.ExceptionMessage}",
			ActionCode = (param.Result == null || param.Result.StatusCode == null ? string.Empty : param.Result.StatusCode.ToString()) ?? string.Empty
		});*/

		[HttpPost("InqueryForRealPerson")]
		public async Task<IActionResult> InqueryForRealPerson()
		{
			if (!RequestIdExists())
				throw StException.RequestIdNotFound();

			return Ok(await ControllerLogic.InqueryForRealPerson());
		}
	}
}