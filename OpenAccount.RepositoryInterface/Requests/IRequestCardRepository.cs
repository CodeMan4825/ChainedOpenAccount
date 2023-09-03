using OpenAccount.Entities.Requests;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Requests
{
	/// <summary>
	/// سفارش کارت
	/// </summary>
	public interface IRequestCardRepository : IBaseRepository<RequestCard, Guid>
	{
	}
}