using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Requests;

namespace OpenAccount.Api.Controllers.PersonData
{
	/// <summary>
	/// اطلاعات شهر - تحصیلات و شغل
	/// </summary>
	public sealed class RealPersonInfoCompletionController : BasePersonController<RealPerson, IRealPersonInfoCompletionBl>
	{
		public RealPersonInfoCompletionController(IConfiguration configuration, IHttpContextAccessor accessor, IRealPersonInfoCompletionBl baseLogic) : base(configuration, accessor, baseLogic, RequestStateType.PersonInfoCompletion)
		{
		}

		/// <summary>
		/// بروزرسانی اطلاعات شهر - تحصیلات و شغل 
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost("PostPersonInfoCompletion")]
		public async Task PostRealPersonInfoCompletion(RealPersonInfoCompletionDto dto) => await ControllerLogic.PostRealPersonInfoCompletion(dto);
	}
}