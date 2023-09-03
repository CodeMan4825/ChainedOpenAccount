using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.Requests;
using System.ComponentModel;

namespace OpenAccount.Entities.Publics.Wallets
{
	/// <summary>
	/// برداشت از کیف پول مشتری
	/// </summary>
	[Description("برداشت از کیف پول مشتری")]
	public sealed class WithdrawalFromWallet : BaseEntity<Guid>
	{
		/// <summary>
		/// درخواست افتتاح حساب
		/// </summary>
		[Comment("درخواست افتتاح حساب")]
		public Guid RequestId { get; set; }
		public Request? Request { get; set; }

		/// <summary>
		/// یوزر آیدی کاربر مبدا
		/// </summary>
		[Comment("یوزر آیدی کاربر مبدا")]
		public string SourceAccount { get; set; } = string.Empty;

		/// <summary>
		/// یوزر آیدی حساب مقصد
		/// </summary>
		[Comment("یوزر آیدی حساب مقصد")]
		public string DestinationAccount { get; set; } = string.Empty;

		/// <summary>
		/// این فیلد یوزر آیدی سازمان را نشان میدهد
		/// </summary>
		[Comment("یوزر آیدی سازمان")]
		public string OrganUserId { get; set; } = string.Empty;

		/// <summary>
		/// نوع تراکنش
		/// </summary>
		[Comment("نوع تراکنش")]
		public string EventType { get; set; } = string.Empty;

		/// <summary>
		/// مبلغ درخواستی تراکنش
		/// </summary>
		[Comment("مبلغ درخواستی تراکنش")]
		public string Amount { get; set; } = string.Empty;

		/// <summary>
		/// شناسه دستگاهی که برای تراکنش درخواست داده شده
		/// </summary>
		[Comment("شناسه دستگاه")]
		public string DeviceId { get; set; } = string.Empty;

		/// <summary>
		/// زمان ارسال درخواست به فرمت 
		/// iso 8601
		/// </summary>
		[Comment("زمان ارسال درخواست")]
		public string RequestDate { get; set; } = string.Empty;

		/// <summary>
		/// با حداکثر طول 100 کاراکتر و شامل شرح تراکنش
		/// </summary>
		[Comment("شرح تراکنش")]
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// شامل کانالی که درخواست از طریق آن انجام شده
		/// </summary>
		[Comment("کانال درخواست")]
		public string Channel { get; set; } = "SET_APP";

		/// <summary>
		/// مبلغ کسر شده استفاده شد؟
		/// </summary>
		[Comment("مبلغ کسر شده استفاده شد؟")]
		public bool Used { get; set; }

		/// <summary>
		/// زمان استفاده از مبلغ
		/// </summary>
		[Comment("زمان استفاده از مبلغ")]
		public DateTime? UseDate { get; set; }

		public string ReferenceNumber { get; set; } = string.Empty;

		public string TraceNumber { get; set; } = string.Empty;

		/// <summary>
		/// در صورت انجام شدن تراکنش این فیلد به عنوان پاسخ به سرویس گیرنده ارائه داده می گردد
		/// به کار ما نمی آید
		/// </summary>
		[Comment("پاسخ به سرویس گیرنده")]
		public string HostRrn { get; set; } = string.Empty;

		/// <summary>
		/// پاسخ سرویس
		/// </summary>
		[Comment("پاسخ سرویس")]
		public string ActionCode { get; set; } = string.Empty;

		/// <summary>
		/// آیا برداشت انجام شده است ؟
		/// </summary>
		public bool ActionCodeOk => ActionCode == "00000";

		/// <summary>
		/// شرح پاسخ
		/// </summary>
		[Comment("شرح پاسخ")]
		public string? ActionMessage { get; set; }

		/// <summary>
		/// سایر خطاها
		/// </summary>
		[Comment("سایر خطاها")]
		public string? ErrorExMessage { get; set; }
	}
}