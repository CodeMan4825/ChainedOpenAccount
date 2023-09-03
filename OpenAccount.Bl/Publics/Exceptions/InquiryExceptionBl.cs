using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.RepositoryInterface.Publics.Exceptions;

namespace OpenAccount.Bl.Publics.Exceptions
{
	/// <summary>
	/// خطاهای اعتبارسنجی
	/// </summary>
	internal sealed class InquiryExceptionBl : BaseExceptionBl<InquiryException, IInquiryExceptionRepository>, IInquiryExceptionBl
	{
		public InquiryExceptionBl(IInquiryExceptionRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}
	}
}