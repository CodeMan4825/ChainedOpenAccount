using OpenAccount.Entities.Requests;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Repository.Requests
{
	/// <summary>
	/// لاگ تغییر وضعیت درخواست از مرحله ای به مرحله ی دیگر
	/// </summary>
	internal sealed class RequestStateLogRepository : ApplicationRepository<RequestStateLog, Guid>, IRequestStateLogRepository
	{
		public RequestStateLogRepository(AppDbContext context) : base(context)
		{
		}
	}
}