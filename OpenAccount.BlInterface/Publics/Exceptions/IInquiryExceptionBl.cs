using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Publics.Exceptions;

namespace OpenAccount.BlInterface.Publics.Exceptions
{
	/// <summary>
	/// خطاهای اعتبارسنجی
	/// </summary>
	public interface IInquiryExceptionBl : IBaseLogic<InquiryException, Guid>
	{
	}
}