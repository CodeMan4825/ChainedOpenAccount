using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Accounts;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Accounts;

namespace OpenAccount.Repository.Accounts
{
	internal sealed class AccountTypeSettingRepository : ApplicationRepository<AccountTypeSetting, short>, IAccountTypeSettingRepository
	{
		public AccountTypeSettingRepository(AppDbContext context) : base(context)
		{
		}

		/// <summary>
		/// تنظیمات این حساب را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		public async Task<AccountTypeSetting?> GetSettingByRequestId(Guid requestId) => await (from s in Entities
																							   join a in Context.RequestAccountTypeSettings on s.Id equals a.AccountTypeSettingId
																							   where a.Id == requestId
																							   select s).AsNoTracking().FirstOrDefaultAsync();
	}
}