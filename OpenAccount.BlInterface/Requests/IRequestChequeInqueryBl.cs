using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Requests.InqueryCheque;

namespace OpenAccount.BlInterface.Requests
{
	/// <summary>
	/// استعلام چک
	/// </summary>
	public interface IRequestChequeInqueryBl : IOpenAccountChainedBl<SamatChequeInquiryRequest, Guid>
	{
		/// <summary>
		/// استعلام چک برای افراد حقیقی
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		Task InqueryForRealPeron(HttpSimorghApiResponseDto<SamatChequeInquiryResponseDto> data);

		/// <summary>
		/// آخرین استعلام چک یک درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>SamatChequeInquiryRequest</returns>
		Task<SamatChequeInquiryRequest?> GetLastInquiry(Guid requestId);
	}
}