using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Requests;

namespace OpenAccount.BlInterface.Requests
{
	/// <summary>
	/// درخواست اولیه ی افتتاح حساب
	/// </summary>
	public interface IRequestStartBl : IOpenAccountChainedBl<Request, Guid>
	{
		/// <summary>
		/// درج درخواست با نوع خاص
		/// </summary>
		/// <param name="accountType"></param>
		/// <returns></returns>
		Task PostForRealPesron(AccountType accountType);
	}
}