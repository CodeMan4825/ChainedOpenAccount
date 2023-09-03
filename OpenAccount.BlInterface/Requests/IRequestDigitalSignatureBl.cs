using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Requests;

namespace OpenAccount.BlInterface.Requests
{
	/// <summary>
	/// امضای دیجیتال هر درخواست
	/// </summary>
	public interface IRequestDigitalSignatureBl : IOpenAccountChainedBl<RequestDigitalSignature, Guid>
	{
		/// <summary>
		/// اطلاعات پرسنلی مورد نیاز امضای دیجیتال
		/// </summary>
		/// <param name="personId"></param>
		/// <returns></returns>
		Task<RealPersonForDSignDto?> GetPersonNeededData(Guid personId);
		
		/// <summary>
		/// فایل تعهدنامه را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task<byte[]> GeneratePdf(Guid requestId);

		/// <summary>
		/// گواهی باراف اولیه ی کاربر
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<string> GetFirstDigest(RequestDigitalSignatureRequestDto dto);

		/// <summary>
		/// گواهی باراف پایانی کاربر
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task PostFinalDigest(RequestDigitalSignatureRequestDto dto);

		/// <summary>
		/// ارسال گواهی به بانک برای پاراف
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task PostToBank(Guid requestId);

		/// <summary>
		/// گواهی پاراف شده ی بانک را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task<byte[]> GetPdfSignedByBank(Guid requestId);

		/// <summary>
		/// ارسال فایل به سرویس مدیریت فایل
		/// </summary>
		/// <param name="formFile"></param>
		/// <returns></returns>
		Task PostToMinIo(ByteArrayContent formFile);

		/// <summary>
		/// آدرس فیزیکی گواهی
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>آدرس</returns>
		Task<string> GetFileAddress(Guid requestId);

		/// <summary>
		/// وضعیت گواهی بارگذاری شده
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task<StatusOfRequestDigitalSignature> GetProcessStatus(Guid requestId);

		/// <summary>
		/// وضعیت استعلام چک و تسهیلات را کنترل می کند که منقضی نشده باشد
		/// </summary>
		/// <param name="requestId"></param>
		/// <exception cref="StException.InquiryNoAcceptable"></exception>
		/// <returns></returns>
		Task GetInqueryExpireState(Guid requestId);
}
}