using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests.InqueryCheque
{
	/// <summary>
	/// اطلاعات چک برگشتی
	/// </summary>
	[Description("اطلاعات چک برگشتی")]	
	public sealed class SamatBouncedChequeItem : BaseEntity<Guid>, ISamatChequeInquiryRequest
	{
		/// <summary>
		/// استعلام چک سمات
		/// </summary>
		[Description("استعلام چک سمات")]
		public Guid SamatChequeInquiryRequestId { get; set; }

		/// <summary>
		/// دلایل برگشت
		/// </summary>
		public List<SamatChequeBouncedReason> BouncedReasons { get; set; } = new();

		/// <summary>
		/// مبلغ چک
		/// </summary>
		[Comment("مبلغ چک")]
		public int Amount { get; set; }

		/// <summary>
		/// کد بانک
		/// </summary>
		[Comment("کد بانک")]
		public int BankCode { get; set; }

		/// <summary>
		/// مبلغ برگشتی
		/// </summary>
		[Comment("مبلغ برگشتی")]
		public int BouncedAmount { get; set; }

		/// <summary>
		/// تاریخ صدور (ارسال) برگشت
		/// </summary>
		[Comment("تاریخ صدور (ارسال) برگشت")]
		public string BouncedDate { get; set; } = string.Empty;

		/// <summary>
		/// کد شعبه برگشت زننده
		/// </summary>
		[Comment("کد شعبه برگشت زننده")]
		public string BranchBounced { get; set; } = string.Empty;

		/// <summary>
		/// کد شعبه افتتاح کننده
		/// </summary>
		[Comment("کد شعبه افتتاح کننده")]
		public string BranchOrigin { get; set; } = string.Empty;

		/// <summary>
		/// نحوه ارائه چک
		/// </summary>
		[Comment("نحوه ارائه چک")]
		public int ChannelKind { get; set; }

		/// <summary>
		/// کد ارز
		/// </summary>
		[Comment("کد ارز")]
		public string CurrencyCode { get; set; } = string.Empty;

		/// <summary>
		/// نرخ ارز
		/// </summary>
		[Comment("نرخ ارز")]
		public decimal CurrencyRate { get; set; }

		/// <summary>
		/// تاریخ سررسید چک
		/// </summary>
		[Comment("تاریخ سررسید چک")]
		public string DeadlineDate { get; set; } = string.Empty;

		/// <summary>
		/// شماره شباي حساب
		/// </summary>
		[Comment("شماره شباي حساب")]
		public string Iban { get; set; } = string.Empty;

		/// <summary>
		/// سریال چک
		/// </summary>
		[Comment("سریال چک")]
		public string Serial { get; set; } = string.Empty;

		/// <summary>
		/// نام شعبه برگشت زننده
		/// </summary>
		[Comment("نام شعبه برگشت زننده")]
		public string BouncedBranchName { get; set; } = string.Empty;

		/// <summary>
		/// نوع مشتری
		/// </summary>
		[Comment("نوع مشتری")]
		public int CustomerType { get; set; }

		/// <summary>
		/// کد رهگیري چک
		/// </summary>
		[Comment("کد رهگیري چک")]
		public int IdCheque { get; set; }

		/// <summary>
		/// نام شعبه افتتاح کننده
		/// </summary>
		[Comment("نام شعبه افتتاح کننده")]
		public string OriginBranchName { get; set; } = string.Empty;
	}
}