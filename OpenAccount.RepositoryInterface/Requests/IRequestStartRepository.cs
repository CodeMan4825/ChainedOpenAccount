using OpenAccount.Entities.Requests;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Requests
{
	/// <summary>
	/// درخواست اولیه ی افتتاح حساب
	/// </summary>
	public interface IRequestStartRepository : IBaseRepository<Request, Guid>
	{
	}
}