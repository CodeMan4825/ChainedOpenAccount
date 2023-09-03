using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.Requests;
using System.ComponentModel;

namespace OpenAccount.Entities.Accounts
{
	/// <summary>
	/// وضعیت کیف پول
	/// </summary>
	[Description("وضعیت کیف پول")]
	public sealed class WalletStatus : BaseEntity<Guid>
	{
		/// <summary>
		/// درخواست افتتاح حساب
		/// </summary>
		public Request Request { get; set; } = new();

		/// <summary>
		/// درخواست افتتاح حساب
		/// </summary>
		[Comment("درخواست افتتاح حساب")]
		public Guid RequestId { get; set; }

		/// <summary>
		/// موجودی واقعی مشتری
		/// </summary>
		[Comment("موجودی واقعی مشتری")]
		public long Balance { get; set; }

		/// <summary>
		/// موجودی مورد نیاز
		/// </summary>
		[Comment("موجودی مورد نیاز")]
		public long NeededBalance { get; set; }

		/// <summary>
		/// تاریخ استعلام موجودی
		/// </summary>
		[Comment("تاریخ استعلام موجودی")]
		public DateTime SysDate { get; set; }

		public string ActionCode { get; set; } = string.Empty;
		public bool ActionCodeOk => string.IsNullOrEmpty(ActionMessage) && ActionCode == "00000";
		public string ActionMessage { get; set; } = string.Empty;
    }
}