using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace OpenAccount.Entities.Publics.Wallets
{
	public sealed class WalletTransferRequestDto
	{
		/// <summary>
		/// یوزر آیدی کاربر مبدا
		/// </summary>
		public string SourceAccount { get; set; } = string.Empty;

		/// <summary>
		/// یوزر آیدی حساب مقصد
		/// </summary>
		public string DestinationAccount { get; set; } = string.Empty;

		/// <summary>
		/// این فیلد یوزر آیدی سازمان را نشان میدهد
		/// </summary>
		public string OrganUserId { get; set; } = string.Empty;

		/// <summary>
		/// نوع تراکنش
		/// </summary>
		public string EventType { get; set; } = string.Empty;

		/// <summary>
		/// مبلغ درخواستی تراکنش
		/// </summary>
		public string Amount { get; set; } = string.Empty;

		/// <summary>
		/// شناسه دستگاهی که برای تراکنش درخواست داده شده
		/// </summary>
		public string DeviceId { get; set; } = string.Empty;

		/// <summary>
		/// زمان ارسال درخواست  به فرمت 
		/// iso 8601
		/// </summary>
		public string RequestDate { get; set; } = string.Empty;

		/// <summary>
		/// با حداکثر طول 100 کاراکتر و شامل شرح تراکنش
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// شامل کانالی که درخواست از طریق آن انجام شده
		/// </summary>
		public string Channel { get; set; } = "BAJET_APP";
	}

	/// <summary>
	/// نوع تراکنش
	/// </summary>
	public enum EventType
	{
		/// <summary>
		/// استعلام ثبت احوال
		/// </summary>
		[Description("استعلام ثبت احوال")]
		IdentityInquiry = 240202,

		/// <summary>
		/// استعلام کدپستی
		/// </summary>
		[Description("استعلام کدپستی")]
		PostalCodeInquiry = 240600,

		/// <summary>
		/// تمبرمالیاتی
		/// </summary>
		[Description("تمبرمالیاتی")]
		StampInquiry = 240405,

		/// <summary>
		/// صدور کارت
		/// </summary>
		[Description("صدور کارت")]
		CardPrice = 240700,

		/// <summary>
		/// ارسال کارت
		/// </summary>
		[Description("ارسال کارت")]
		CardSendPrice = 240705,

		/// <summary>
		/// کارمزد اتصال کارت به حساب
		/// </summary>
		[Description("کارمزد اتصال کارت به حساب")]
		CardToAccount = 240703,

		/// <summary>
		/// افتتاح حساب
		/// </summary>
		[Description("افتتاح حساب")]
		OpenAccount = 111111
	}
}