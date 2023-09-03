using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Publics.Exceptions;

namespace OpenAccount.Repository.Publics.Exceptions
{
	/// <summary>
	/// خطاهای اعتبارسنجی
	/// </summary>
	internal sealed class InquiryExceptionRepository : BaseExceptionRepository<InquiryException>, IInquiryExceptionRepository
	{
		public InquiryExceptionRepository(AppDbContext context) : base(context)
		{
		}
	}
}