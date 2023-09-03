using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Publics.Wallets;

namespace OpenAccount.Repository.Publics.Wallets
{
	/// <summary>
	/// برداشت از کیف پول مشتری
	/// </summary>
	internal sealed class WithdrawalFromWalletRepository : ApplicationRepository<WithdrawalFromWallet, Guid>, IWithdrawalFromWalletRepository
	{
		public WithdrawalFromWalletRepository(AppDbContext context) : base(context)
		{
		}

		public override Task Add(WithdrawalFromWallet entity, bool save = true)
		{
			if (entity.Request != null && entity.Request.RequestStateLogs != null && entity.Request.RequestStateLogs.Any())
			{
				foreach (var item in entity.Request.RequestStateLogs)
					Context.Attach(item).State = EntityState.Added;
				Context.Attach(entity.Request).State = EntityState.Modified;
			}
			return base.Add(entity, save);
		}

		protected override void SetFieldsForUpdate(WithdrawalFromWallet foundObj, WithdrawalFromWallet data)
		{
			foundObj.Used = data.Used;
			foundObj.UseDate = data.UseDate;
		}
	}
}