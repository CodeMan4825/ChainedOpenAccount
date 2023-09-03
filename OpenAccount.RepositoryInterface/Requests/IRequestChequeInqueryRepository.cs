using OpenAccount.Entities.Requests.InqueryCheque;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Requests
{
	/// <summary>
	/// استعلام چک
	/// </summary>
	public interface IRequestChequeInqueryRepository : IBaseRepository<SamatChequeInquiryRequest, Guid>
	{
		/// <summary>
		/// آخرین استعلام چک یک درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>SamatChequeInquiryRequest</returns>
		Task<SamatChequeInquiryRequest?> GetLastInquiry(Guid requestId);
	}
}