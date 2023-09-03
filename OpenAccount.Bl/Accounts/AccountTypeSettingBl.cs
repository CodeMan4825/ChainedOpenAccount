using Microsoft.AspNetCore.Http;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.Entities.Accounts;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Accounts;

namespace OpenAccount.Bl.Accounts
{
	internal sealed class AccountTypeSettingBl : BaseLogic<AccountTypeSetting, IAccountTypeSettingRepository, short>, IAccountTypeSettingBl
	{
		public AccountTypeSettingBl(IAccountTypeSettingRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}

		/// <summary>
		/// تنظیمات فعال هر نوع حساب را برمی گرداند
		/// </summary>
		/// <returns>List<AccountTypeSetting></returns>
		public Task<List<AccountTypeSetting>> GetActiveSettings() => Task.FromResult(LogicRepository.AsQuery().Where(x => x.IsActive).ToList());

		/// <summary>
		/// تنظیمات فعال نوع حساب خاصی را برمی گرداند
		/// </summary>
		/// <param name="accountType">نوع حساب</param>
		/// <returns>AccountTypeSetting or null</returns>
		public Task<AccountTypeSetting?> GetActiveSettingsByType(AccountType accountType) => Task.FromResult(LogicRepository.AsQuery().FirstOrDefault(x => x.IsActive && x.AccountType == accountType));

		/// <summary>
		/// موجودی مورد نیاز این حساب را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		public async Task<AccountTypeSetting> GetSettingByRequestId(Guid requestId) =>
			await LogicRepository.GetSettingByRequestId(requestId) ?? throw StException.DataNotFound("تنظیمات حساب");

		public Task<long> CalcNeededBalance(AccountTypeSetting s) =>
			Task.FromResult(s.MinBalance + s.Stamp + s.InqueryPrice + s.IdentificationInquiry + s.PostalCodeInquiry + s.CardPrice + s.CardSendPrice);

		public override Task Post(AccountTypeSetting entity, bool save = true)
		{
			var find = LogicRepository.AsQuery().FirstOrDefault(x => x.AccountType == entity.AccountType);
			// از پیش برای این نوع حساب تنظیماتی وجود داشت
			if (find != null)
			{
				find.IsActive = false;
				LogicRepository.Update(find, false);
			}

			return base.Post(entity.Clone(), save);
		}
	}
}