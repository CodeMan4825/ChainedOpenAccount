namespace OpenAccount.Entities.Accounts
{
	public sealed class WalletStatusResponseDto : WalletStatusResponseBriefDto
	{
		/// <summary>
		/// شماره موبایل
		/// </summary>
		public string PhoneNo { get; set; } = string.Empty;

		/// <summary>
		/// موجودی کیف دوم
		/// </summary>
		public long UnlimitBalance { get; set; }
    }

	public class WalletStatusResponseBriefDto
	{
		/// <summary>
		/// وضعیت
		/// </summary>
		public string Status { get; set; } = string.Empty;

		/// <summary>
		/// موجودی کیف پول
		/// </summary>
		public long CurrentBalance { get; set; } = 0;

		/// <summary>
		/// توضیحات
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// مجموع هزینه ها
		/// </summary>
		public long NeededBalance { get; set; }

		/// <summary>
		/// تشکیل پرونده
		/// </summary>
		public long Filing { get; set; }

		/// <summary>
		/// موجودی اولیه
		/// </summary>
		public long InitialBalance { get; set; }

		/// <summary>
		/// شارژ مورد نیاز
		/// </summary>
		public long NeededCharge { get; set; }
	}
}