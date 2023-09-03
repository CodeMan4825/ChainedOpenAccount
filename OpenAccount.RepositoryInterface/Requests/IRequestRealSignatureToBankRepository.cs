using OpenAccount.Entities.Requests;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Requests
{
	/// <summary>
	/// ارسال پاراف خیس به بانک
	/// </summary>
	public interface IRequestRealSignatureToBankRepository : IBaseRepository<RequestRealSignatureToBank, Guid>
	{
	}
}