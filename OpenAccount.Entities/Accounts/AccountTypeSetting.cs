using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.Requests;
using System.ComponentModel;

namespace OpenAccount.Entities.Accounts
{
	/// <summary>
	/// تنظیمات هر نوع حساب
	/// </summary>
	[Description("تنظیمات هر نوع حساب")]
	public sealed class AccountTypeSetting : BaseEntity<short>
	{
		/// <summary>
		/// نوع حساب
		/// </summary>
		[Comment("نوع حساب")]
		public AccountType AccountType { get; set; }

		/// <summary>
		/// گروه حساب
		/// </summary>
		[Comment("گروه حساب")]
		public string AccountGroupId { get; set; } = string.Empty;

		/// <summary>
		/// شرح نوع حساب
		/// </summary>
		[Comment("شرح نوع حساب")]
		public string AccountTypeTitle { get; set; } = string.Empty;

		/// <summary>
		/// موجودی اولیه حساب
		/// </summary>
		[Comment("موجودی اولیه حساب")]
		public long MinBalance { get; set; }

		/// <summary>
		/// استعلام چک و تسهیلات معوق - سمات و سماچک
		/// </summary>
		[Comment("استعلام چک و تسهیلات معوق - سمات و سماچک")]
		public long InqueryPrice { get; set; }

		/// <summary>
		/// استعلام ثبت احوال
		/// </summary>
		[Comment("استعلام ثبت احوال")]
		public long IdentificationInquiry { get; set; } = 0;

		/// <summary>
		/// استعلام کد پستی
		/// </summary>
		[Comment("استعلام کد پستی")]
		public long PostalCodeInquiry { get; set; } = 0;

		/// <summary>
		/// تمبر
		/// </summary>
		[Comment("تمبر")]
		public int Stamp { get; set; } = 0;

        /// <summary>
        /// این تنظیمات برای این نوع حساب فعال است؟
        /// </summary>
        [Comment("این تنظیمات برای این نوع حساب فعال است؟")]
		public bool IsActive { get; set; }

		/// <summary>
		/// هزینه صدور کارت
		/// </summary>
		[Comment("هزینه صدور کارت")]
		public long CardPrice { get; set; }

		/// <summary>
		/// هزینه ارسال کارت
		/// </summary>
		[Comment("هزینه ارسال کارت")]
		public long CardSendPrice { get; set; }

		/// <summary>
		/// کارمزد اتصال کارت به حساب
		/// </summary>
		[Comment("کارمزد اتصال کارت به حساب")]
		public long CardToAccount { get; set; }

        /// <summary>
        /// کمینه ی زندگی
        /// </summary>
        [Comment("کمینه ی زندگی")]
		public byte MinAge { get; set; }

		/// <summary>
		/// بیشینه ی زندگی
		/// </summary>
		[Comment("بیشینه ی زندگی")]
		public byte MaxAge { get; set; }

        public DateTime SysDate { get; set; }

		public override AccountTypeSetting Clone() => (AccountTypeSetting)MemberwiseClone();

		/// <summary>
		/// تنظیماتی که برای هر درخواست افتتاح حساب است
		/// </summary>
		public ICollection<RequestAccountTypeSetting>? RequestAccountTypeSettings { get; set; }
	}
}