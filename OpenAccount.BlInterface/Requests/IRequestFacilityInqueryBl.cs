using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Requests.InqueryCheque;
using OpenAccount.Entities.Requests.InqueryLoan;

namespace OpenAccount.BlInterface.Requests
{
	/// <summary>
	/// استعلام تسهیلات
	/// </summary>
	public interface IRequestFacilityInqueryBl : IOpenAccountChainedBl<SamatLoanInquiryRequest, Guid>
	{
		/// <summary>
		/// استعلام تسهیلات و چک برای افراد حقیقی
		/// </summary>
		/// <returns>تسهیلات و چک مشکل دار</returns>
		Task<AllInquiryResponseDto> InqueryForRealPerson();

		/// <summary>
		/// آخرین استعلام تسهیلات یک درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>SamatChequeInquiryRequest</returns>
		Task<SamatLoanInquiryRequest?> GetLastInquiry(Guid requestId);
	}
}