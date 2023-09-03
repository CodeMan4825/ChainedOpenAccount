using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.PersonData
{
	/// <summary>
	/// اطلاعات تکمیلی شخص
	/// </summary>
	[Description("اطلاعات تکمیلی شخص")]
	public class PersonInfo : BaseEntity<Guid>
	{
		[Comment("کدخطا در دریافت اطلاعات تکمیلی")]
		public string ErrorCode { get; set; } = string.Empty;

		[Comment("شرح خطا در دریافت اطلاعات تکمیلی")]
		public string ErrorMessage { get; set; } = string.Empty;

		[Comment("کدپستی")]
		public string PostalCode { get; set; } = string.Empty;

		/// <summary>
		/// زمان ثبت اطلاعات
		/// </summary>
		[Comment("زمان ثبت اطلاعات")]
		public DateTime SysDate { get; set; }

		/// <summary>
		/// آخرین اطلاعات خوانده شده
		/// </summary>
		[Comment("آخرین اطلاعات خوانده شده")]
		public bool IsActive { get; set; }
	}
}