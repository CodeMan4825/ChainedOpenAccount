using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Publics.Exceptions
{
	/// <summary>
	/// خطاهای اعتبارسنجی
	/// </summary>
	public interface IInquiryExceptionRepository : IBaseExceptionRepository<InquiryException>
	{
	}
}