using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Requests;

namespace OpenAccount.BlInterface.Requests
{
	public interface IRequestBl : IBaseLogic<Request, Guid>
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
		/// Get request of user by account type.
		/// </summary>
		/// <param name="accountType"></param>
		/// <returns>Request</returns>
		Task<Request?> GetUserRequestByAccountType(AccountType accountType);
		
		/// <summary>
		/// لیست حساب هایی که برای افتتاح حساب در دسترسند
		/// </summary>
		/// <returns>لیست حساب ها</returns>
		Task<IEnumerable<AvalableRequestsDto>> GetAvalableRequests();

		/// <summary>
		/// شماره حساب را بده
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task<string> GetAccountNumber(Guid requestId);
	}
}