using OpenAccount.Entities.Accounts;

namespace OpenAccount.BlInterface.Accounts
{
	/// <summary>
	/// افتتاح حساب کاربر
	/// </summary>
	public interface IUserAccountBl : IOpenAccountChainedBl<UserAccount, Guid>
	{
		/// <summary>
		/// افتتاح حساب
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>شماره حساب</returns>
		Task<string> OpenAccount(Guid requestId);
	}
}