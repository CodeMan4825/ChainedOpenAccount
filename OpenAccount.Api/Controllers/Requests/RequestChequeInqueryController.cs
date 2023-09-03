/*using Microsoft.AspNetCore.Mvc;
using OpenAccount.Api.Infrastructure;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Requests;
using OpenAccount.Entities.Requests.InqueryCheque;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers.Requests
{
	/// <summary>
	/// استعلام چک
	/// </summary>
	public sealed class RequestChequeInqueryController : OpenAccountChainedController<SamatChequeInquiryRequest, IRequestChequeInqueryBl, Guid>
	{
		public RequestChequeInqueryController(IConfiguration configuration, IHttpContextAccessor accessor, IRequestChequeInqueryBl baseLogic) : base(configuration, accessor, baseLogic, RequestStateType.ChequeInquery)
		{
		}

		public override async Task HandledExceptions([FromBody] HandleExceptionParam param) => await ControllerLogic.InqueryForRealPeron(new()
		{
			ActionMessage = $"{param.Result?.Value?.ToString()} : {param.ExceptionMessage}",
			ActionCode = (param.Result == null || param.Result.StatusCode == null ? string.Empty : param.Result.StatusCode.ToString()) ?? string.Empty
		});

		[HttpPost("InqueryForRealPeron")]
		public async Task<IActionResult> InqueryForRealPeron() => await DoAction(async () =>
		{
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var result = await HttpClients.Post<HttpSimorghApiResponseDto<SamatChequeInquiryResponseDto>>(client,
				CnfgValue("Btms:MainUrl"),
				CnfgValue("Btms:SamatChequeInquiry"),
				new
				{
					requestInfo = RequestId,
					personInfo = new
					{
						nationalId = UserData.NationalCode,
						personType = 1
					}
				});
			await ControllerLogic.InqueryForRealPeron(result);
		});
	}
}*/