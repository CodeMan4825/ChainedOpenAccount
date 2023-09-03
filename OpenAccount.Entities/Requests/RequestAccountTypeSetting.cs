using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests
{
	/// <summary>
	/// تنظیماتی که برای هر درخواست افتتاح حساب است
	/// </summary>
	[Description("تنظیماتی که برای هر درخواست افتتاح حساب است")]
	public sealed class RequestAccountTypeSetting : BaseEntity<Guid>
	{
		/// <summary>
		/// تنظیمات هر نوع حساب
		/// </summary>
		[Comment("تنظیمات هر نوع حساب")]
		public short AccountTypeSettingId { get; set; }

		/// <summary>
		/// تنظیمات هر نوع حساب
		/// </summary>
		public AccountTypeSetting? AccountTypeSetting { get; set; }

		/// <summary>
		/// درخواست
		/// </summary>
		public Request? Request { get; set; }
	}
}