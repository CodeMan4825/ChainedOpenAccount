using System.ComponentModel;

namespace OpenAccount.Entities.Accounts
{
	/// <summary>
	/// انواع حساب
	/// </summary>
	public enum AccountType : byte
	{
		/// <summary>
		/// کوتاه مدت
		/// </summary>
		[Description("کوتاه مدت")]
		ShortTermAccount = 0,

		/// <summary>
		/// قرض الحسنه
		/// </summary>
		[Description("قرض الحسنه")]
		LoanAccount = 1,

		/// <summary>
		/// مرابحه
		/// </summary>
		[Description("مرابحه")]
		ProfitAccount = 2,

		/// <summary>
		/// جاری
		/// </summary>
		[Description("جاری")]
		CurrentAccount = 3
	}
}