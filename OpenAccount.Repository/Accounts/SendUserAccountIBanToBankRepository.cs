using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Accounts;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Accounts;

namespace OpenAccount.Repository.Accounts
{
	/// <summary>
	/// ارسال شبای حساب به بانک
	/// </summary>
	internal sealed class SendUserAccountIBanToBankRepository : ApplicationRepository<UserAccount, Guid>, ISendUserAccountIBanToBankRepository
	{
		public SendUserAccountIBanToBankRepository(AppDbContext context) : base(context)
		{
		}

		public override Task Update(UserAccount entity, bool save = true)
		{
			if (entity.UserAccountLogs != null && entity.UserAccountLogs.Any()) // اگر لاگ خطا داشت
				foreach (var log in entity.UserAccountLogs)
					Context.Attach(log).State = EntityState.Added;
			else if (!string.IsNullOrEmpty(entity.ShebaNumber)) // اگر لاگ خطا نداشت و شبا داشت برو مرحله ی بعد
			{
				Context.Attach(entity.Request).State = EntityState.Modified;
				Context.Attach(entity.Request.RequestStateLogs.First()).State = EntityState.Added;
			}
			return base.Update(entity, save);
		}

		protected override void SetFieldsForUpdate(UserAccount foundObj, UserAccount data) => foundObj.ShebaNumber = data.ShebaNumber;
	}
}