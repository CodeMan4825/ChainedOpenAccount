using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers.PersonData
{
	/// <summary>
	/// استعلام کدپستی
	/// </summary>
	public sealed class RealPersonPostInqueryController : BasePersonController<RealPerson, IRealPersonPostInqueryBl>
	{
		public RealPersonPostInqueryController(IConfiguration configuration,IHttpContextAccessor accessor, IRealPersonPostInqueryBl baseLogic) : base(configuration, accessor, baseLogic, RequestStateType.PersonPostInquery)
		{
		}

		/// <summary>
		/// استعلام کدپستی بصورت آفلاین
		/// </summary>
		/// <returns>FullAddress</returns>
		[HttpGet("PostalCodeOfflineInquiry")]
		public async Task<IActionResult> PostalCodeOfflineInquiry()
		{
			if (!RequestIdExists())
				throw StException.RequestIdNotFound();

			return Ok(await ControllerLogic.PostalCodeOfflineInquiry());
		}

		/// <summary>
		/// استعلام کدپستی
		/// </summary>
		/// <param name="postalCode">کد پستی</param>
		/// <returns></returns>
		[HttpPost("PostalCodeInquiry")]
		public async Task<IActionResult> PostalCodeInquiry(string postalCode)
		{
			if (!RequestIdExists())
				throw StException.RequestIdNotFound();

			if (string.IsNullOrEmpty(postalCode.Trim()))
				throw StException.ArgumentNull("کدپستی");

			return Ok(await ControllerLogic.PostInquiry(postalCode));
		}		
	}
}