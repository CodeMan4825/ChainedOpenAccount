/*using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers.Requests
{
	/// <summary>
	/// پاراف خیس
	/// </summary>
	public sealed class RequestRealSignatureController : OpenAccountChainedController<RequestRealSignature, IRequestRealSignatureBl, Guid>
	{
		public RequestRealSignatureController(IConfiguration configuration,
			IHttpContextAccessor accessor,
			IRequestRealSignatureBl baseLogic) :
			base(configuration, accessor, baseLogic, RequestStateType.RealSignature)
		{
		}

		/// <summary>
		/// Uploads sign in MinIo.
		/// </summary>
		/// <param name="sign"></param>
		/// <returns></returns>
		[HttpPost("UpLoad")]
		public async Task<IActionResult> UpLoad([FromBody] RequestRealSignatureDto sign) => await DoAction(async () =>
		{
			if (sign == null || string.IsNullOrEmpty(sign.SignatureInBase64))
				throw StException.ArgumentNull("تصویر");
			
			await ControllerLogic.Upload(sign);
		});
	}
}*/