using OpenAccount.Entities.Accounts;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Accounts
{
	public interface IAccountTypeSettingRepository : IBaseRepository<AccountTypeSetting, short>
	{
		/// <summary>
		/// تنظیمات این حساب را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		Task<AccountTypeSetting?> GetSettingByRequestId(Guid requestId);
	}
}