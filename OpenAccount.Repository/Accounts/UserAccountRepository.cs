using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Accounts;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Accounts;

namespace OpenAccount.Repository.Accounts
{
	/// <summary>
	/// افتتاح حساب کاربر
	/// </summary>
	internal sealed class UserAccountRepository : ApplicationRepository<UserAccount, Guid>, IUserAccountRepository
	{
		public UserAccountRepository(AppDbContext context) : base(context)
		{
		}

		public override Task Add(UserAccount entity, bool save = true)
		{
			if (entity.UserAccountLogs != null && entity.UserAccountLogs.Any())
				foreach (var item in entity.UserAccountLogs)
					Context.Attach(item).State = EntityState.Added;
			if (entity.Request.RequestStateLogs != null && entity.Request.RequestStateLogs.Any())
			{
				foreach (var item in entity.Request.RequestStateLogs)
					Context.Attach(item).State = EntityState.Added;
				Context.Attach(entity.Request).State = EntityState.Modified;
			}
			return base.Add(entity, save);
		}

		public override async Task Update(UserAccount entity, bool save = true)
		{
			if (entity.UserAccountLogs != null && entity.UserAccountLogs.Any())
				foreach (var item in entity.UserAccountLogs)
					Context.Attach(item).State = EntityState.Added;
			if (entity.Request.RequestStateLogs != null && entity.Request.RequestStateLogs.Any())
			{
				foreach (var item in entity.Request.RequestStateLogs)
					Context.Attach(item).State = EntityState.Added;
				Context.Attach(entity.Request).State = EntityState.Modified;
			}
			Context.Attach(entity).State = EntityState.Modified;
			if (save)
				await SaveChangesAsync();
		}

		protected override void SetFieldsForUpdate(UserAccount foundObj, UserAccount data)
		{
			foundObj.ShebaNumber = data.ShebaNumber;
			foundObj.AccountNumber = data.AccountNumber;
		}
	}
}