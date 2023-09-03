using OpenAccount.Entities.Requests.InqueryLoan;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Requests
{
	/// <summary>
	/// استعلام تسهیلات
	/// </summary>
	public interface IRequestFacilityInqueryRepository : IBaseRepository<SamatLoanInquiryRequest, Guid>
	{
		/// <summary>
		/// آخرین استعلام تسهیلات یک درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>SamatLoanInquiryRequest</returns>
		Task<SamatLoanInquiryRequest?> GetLastInquiry(Guid requestId);
	}
}