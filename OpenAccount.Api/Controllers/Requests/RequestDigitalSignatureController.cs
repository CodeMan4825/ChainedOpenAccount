using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers.Requests
{
	/// <summary>
	/// امضای دیجیتال هر درخواست
	/// </summary>
	public sealed class RequestDigitalSignatureController : OpenAccountChainedController<RequestDigitalSignature, IRequestDigitalSignatureBl, Guid>
	{
		public RequestDigitalSignatureController(IConfiguration configuration, IHttpContextAccessor accessor, IRequestDigitalSignatureBl baseLogic) :
			base(configuration, accessor, baseLogic, RequestStateType.DigitalSignature)
		{ }

		/// <summary>
		/// اطلاعات پرسنلی مورد نیاز امضای دیجیتال
		/// </summary>
		/// <param name="personId"></param>
		/// <returns></returns>
		[HttpGet("GetPersonNeededData")]
		public async Task<RealPersonForDSignDto?> GetPersonNeededData() => await ControllerLogic.GetPersonNeededData(UserData.UserId) ?? throw StException.DataNotFound("اطلاعات پرسنلی");

		/// <summary>
		/// گواهی باراف اولیه ی کاربر
		/// </summary>
		/// <param name="signerCertificate"></param>
		/// <returns></returns>
		[HttpGet("GetFirstDigest")]
		public async Task<IActionResult> GetFirstDigest(RequestDigitalSignatureRequestDto dto) => await GetAction(async () =>
		{
			if (string.IsNullOrEmpty(dto.SignerCertificate))
				throw StException.ArgumentNull("گواهی امضاء خالی می باشد");
			
			return await ControllerLogic.GetFirstDigest(dto);
		});

		/// <summary>
		/// گواهی باراف پایانی کاربر
		/// بعلاوه باراف بانک
		/// </summary>
		/// <param name="signature"></param>
		/// <returns></returns>
		[HttpPost("FinalizeDigest")]
		public async Task FinalizeDigest(RequestDigitalSignatureRequestDto dto)
		{
			if (string.IsNullOrEmpty(dto.SignerCertificate))
				throw StException.ArgumentNull("گواهی امضاء");

			/// وضعیت استعلام چک و تسهیلات را کنترل می کند که منقضی نشده باشد
			await ControllerLogic.GetInqueryExpireState(RequestId);

			await ControllerLogic.PostFinalDigest(dto);
			//await ControllerLogic.PostToBank(RequestId);

			var bytes = await ControllerLogic.GetPdfSignedByBank(RequestId);
			var byteContent = new ByteArrayContent(bytes);
			await ControllerLogic.PostToMinIo(byteContent);
		}

		/// <summary>
		/// آدرس فیزیکی گواهی
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetFileAddress")]
		public async Task<IActionResult> GetFileAddress() => Ok(await ControllerLogic.GetFileAddress(RequestId));

		/// <summary>
		/// وضعیت گواهی بارگذاری شده
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetProcessStatus")]
		public async Task<IActionResult> GetProcessStatus() => Ok(await ControllerLogic.GetProcessStatus(RequestId));
	}
}