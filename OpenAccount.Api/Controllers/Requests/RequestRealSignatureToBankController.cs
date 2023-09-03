using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Requests;

namespace OpenAccount.Api.Controllers.Requests
{
	/// <summary>
	/// ارسال پاراف خیس به بانک
	/// </summary>
	public sealed class RequestRealSignatureToBankController : OpenAccountChainedController<RequestRealSignatureToBank, IRequestRealSignatureToBankBl, Guid>
	{
		public RequestRealSignatureToBankController(IConfiguration configuration, IHttpContextAccessor accessor, IRequestRealSignatureToBankBl baseLogic) : 
			base(configuration, accessor, baseLogic, RequestStateType.RealSignatureToBank)
		{			
		}

		/// <summary>
		/// ارسال امضای خیس به بانک
		/// </summary>
		/// <returns></returns>
		[HttpPost("SendToBank")]
		public async Task SendToBank() => await ControllerLogic.SendToBank(RequestId);
	}
}