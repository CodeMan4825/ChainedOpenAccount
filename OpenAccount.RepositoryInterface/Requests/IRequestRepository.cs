using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Requests;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Requests
{
	public interface IRequestRepository : IBaseRepository<Request, Guid>
	{
		/// <summary>
		/// تنظیمات فعال درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId">شناسه درخواست</param>
		/// <returns></returns>
		Task<AccountTypeSetting> GetAccountTypeSetting(Guid requestId);

		/// <summary>
		/// Get request with RequestLog and PersonInfo data.
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task<Request> GetWithLogPersonInfo(Guid requestId);

		/// <summary>
		/// اطلاعات افتتاح حساب کاربر
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task<UserAccount> GetUserAccount(Guid requestId);

		/// <summary>
		/// شماره حساب را بده
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task<string> GetAccountNumber(Guid requestId);
	}
}