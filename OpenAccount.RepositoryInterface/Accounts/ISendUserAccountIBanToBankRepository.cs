using OpenAccount.Entities.Accounts;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Accounts
{
	/// <summary>
	/// ارسال شبای حساب به بانک
	/// </summary>
	public interface ISendUserAccountIBanToBankRepository : IBaseRepository<UserAccount, Guid>
	{
	}
}