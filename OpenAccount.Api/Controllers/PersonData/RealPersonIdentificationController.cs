using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers.PersonData
{
	/// <summary>
	/// شناسائی هویت افراد حقیقی
	/// </summary>
	public sealed class RealPersonIdentificationController : BasePersonController<RealPerson, IRealPersonIdentificationBl>
	{
		public RealPersonIdentificationController(
			IConfiguration configuration,
			IHttpContextAccessor accessor,
			IRealPersonIdentificationBl baseLogic) : base(configuration, accessor, baseLogic, RequestStateType.PersonIdentification) { }

		/// <summary>
		/// استعلام ثبت احوال
		/// Offline
		/// </summary>
		/// <returns>IdentityInquiryResult</returns>
		[HttpGet("IdentityInquiry")]
		public async Task<IActionResult> IdentityInquiry()
		{
			if (!RequestIdExists())
				throw StException.RequestIdNotFound();

			await ControllerLogic.IdentityInquiry();
			return Ok();
		}

		/// <summary>
		/// آیا کاربر زنده است؟
		/// </summary>
		/// <returns></returns>
		/// <exception cref="StException.AccessDenied">دسترسی غیرمجاز - کاربر زنده نیست</exception>
		/// <exception cref="StException.ServiceUnavailable">احراز هویت</exception>
		/// <exception cref="StException.DataNotFound">اطلاعات پرسنلی</exception>
		/// <exception cref="StException.ResultNotAcceptable">service ActionCode</exception>
		[HttpGet("IsUserAlive")]
		public async Task IsUserAlive() => await ControllerLogic.IsUserAlive();
	}
}