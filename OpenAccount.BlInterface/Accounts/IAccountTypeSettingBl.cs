using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Accounts;

namespace OpenAccount.BlInterface.Accounts
{
	public interface IAccountTypeSettingBl : IBaseLogic<AccountTypeSetting, short>
	{
		/// <summary>
		/// تنظیمات فعال هر نوع حساب را برمی گرداند
		/// </summary>
		/// <returns>List<AccountTypeSetting></returns>
		Task<List<AccountTypeSetting>> GetActiveSettings();

		/// <summary>
		/// تنظیمات فعال نوع حساب خاصی را برمی گرداند
		/// </summary>
		/// <param name="accountType">نوع حساب</param>
		/// <returns>AccountTypeSetting or null</returns>
		Task<AccountTypeSetting?> GetActiveSettingsByType(AccountType accountType);

		/// <summary>
		/// تنظیمات این حساب را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		Task<AccountTypeSetting> GetSettingByRequestId(Guid requestId);

		Task<long> CalcNeededBalance(AccountTypeSetting s);
	}
}