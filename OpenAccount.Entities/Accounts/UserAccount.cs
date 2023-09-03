using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.Requests;
using System.ComponentModel;

namespace OpenAccount.Entities.Accounts
{
	/// <summary>
	/// افتتاح حساب کاربر
	/// Id = Request.Id
	/// </summary>
	[Description("افتتاح حساب کاربر")]
	public sealed class UserAccount : BaseEntity<Guid>
	{
		public Request Request { get; set; } = new();

        /// <summary>
        /// شماره حساب
        /// </summary>
        [Comment("شماره حساب")]
		public string AccountNumber { get; set; } = string.Empty;

		/// <summary>
		/// شماره شبا
		/// </summary>
		[Comment("شماره شبا")]
		public string ShebaNumber { get; set; } = string.Empty;

		/// <summary>
		/// تاریخ افتتاح حساب
		/// </summary>
		[Comment("تاریخ افتتاح حساب")]
		public DateTime SysDate { get; set; }

		/// <summary>
		/// لاگ افتتاح حساب کاربر
		/// </summary>
		public ICollection<UserAccountLog>? UserAccountLogs { get; set; }
    }
}