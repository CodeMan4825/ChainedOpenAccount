using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Accounts;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Accounts;

namespace OpenAccount.Repository.Accounts
{
	/// <summary>
	/// وضعیت کیف پول
	/// </summary>
	internal sealed class WalletStatusRepository : ApplicationRepository<WalletStatus, Guid>, IWalletStatusRepository
	{
		public WalletStatusRepository(AppDbContext context) : base(context)
		{
		}

		public override Task Add(WalletStatus entity, bool save = true)
		{
			if (entity.Request.RequestStateLogs != null && entity.Request.RequestStateLogs.Any())
			{
				Context.Attach(entity.Request).State = EntityState.Modified;
				Context.Attach(entity.Request.RequestStateLogs.First()).State = EntityState.Added;
			}
			return base.Add(entity, save);
		}
	}
}