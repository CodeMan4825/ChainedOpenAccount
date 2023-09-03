using Microsoft.EntityFrameworkCore;

namespace OpenAccount.Entities.Requests.InqueryCheque
{
    public interface ISamatChequeInquiryRequest
    {
		/// <summary>
		/// مبلغ چک
		/// </summary>
		[Comment("مبلغ چک")]
		int Amount { get; set; }

		/// <summary>
		/// کد بانک
		/// </summary>
		[Comment("کد بانک")]
		int BankCode { get; set; }

		/// <summary>
		/// مبلغ برگشتی
		/// </summary>
		[Comment("مبلغ برگشتی")]
		int BouncedAmount { get; set; }

		/// <summary>
		/// تاریخ صدور ( ارسال ) برگشت
		/// </summary>
		[Comment("تاریخ صدور ( ارسال ) برگشت")]
		string BouncedDate { get; set; }

		/// <summary>
		/// کد شعبه برگشت زننده
		/// </summary>
		[Comment("کد شعبه برگشت زننده")]
		string BranchBounced { get; set; }

		/// <summary>
		/// کد شعبه افتتاح کننده
		/// </summary>
		[Comment("کد شعبه افتتاح کننده")]
		string BranchOrigin { get; set; }

		/// <summary>
		/// نحوه ارائه چک
		/// </summary>
		[Comment("نحوه ارائه چک : یک = چکاوک، دو = مراجعه ذینفع به بانک عهده")]
		int ChannelKind { get; set; }

		/// <summary>
		/// نحوه ارائه چک
		/// </summary>
		string ChannelKindName => ChannelKind == 1 ? "چکاوك" : (ChannelKind == 2 ? "مراجعه ذینفع به بانک عهده" : string.Empty);

		/// <summary>
		/// کد ارز
		/// </summary>
		[Comment("کد ارز")]
		string CurrencyCode { get; set; }

		/// <summary>
		/// نرخ ارز
		/// </summary>
		[Comment("نرخ ارز")]
		decimal CurrencyRate { get; set; }

		/// <summary>
		/// تاریخ چک - سررسید
		/// </summary>
		[Comment("تاریخ چک - سررسید")]
		string DeadlineDate { get; set; }

		/// <summary>
		/// شماره شباي حساب
		/// </summary>
		[Comment("شماره شباي حساب")]
		string Iban { get; set; }

		/// <summary>
		/// سریال چک
		/// </summary>
		[Comment("سریال چک")]
		string Serial { get; set; }

		/// <summary>
		/// نام شعبه برگشت زننده
		/// </summary>
		[Comment("نام شعبه برگشت زننده")]
		string BouncedBranchName { get; set; }

		/// <summary>
		/// نوع مشتری
		/// </summary>
		[Comment("نوع مشتری: یک = صاحب حساب، دو = امضا کننده و سه = ذینفع")]
		int CustomerType { get; set; }

		/// <summary>
		/// نوع مشتری
		/// </summary>
		string CustomerTypeName => CustomerType switch
		{
			1 => "صاحب حساب",
			2 => "امضا کننده",
			3 => "ذینفع",
			_ => string.Empty,
		};

		/// <summary>
		/// کد رهگیري چک
		/// </summary>
		[Comment("کد رهگیري چک")]
		int IdCheque { get; set; }

		/// <summary>
		/// نام شعبه افتتاح کننده
		/// </summary>
		[Comment("نام شعبه افتتاح کننده")]
		string OriginBranchName { get; set; }
	}
}