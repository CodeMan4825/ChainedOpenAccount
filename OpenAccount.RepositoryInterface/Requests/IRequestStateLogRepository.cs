using OpenAccount.Entities.Requests;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Requests
{
	/// <summary>
	/// لاگ تغییر وضعیت درخواست از مرحله ای به مرحله ی دیگر
	/// </summary>
	public interface IRequestStateLogRepository : IBaseRepository<RequestStateLog, Guid>
	{
	}
}