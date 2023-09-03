namespace OpenAccount.Entities.Requests.InqueryCheque
{
    public sealed class SamatBouncedChequeItemDto : ISamatChequeInquiryRequest
    {
		/// <summary>
		/// مبلغ چک
		/// </summary>
		public int Amount { get; set; }

		/// <summary>
		/// کد بانک
		/// </summary>
		public int BankCode { get; set; }

		/// <summary>
		/// مبلغ برگشتی
		/// </summary>
		public int BouncedAmount { get; set; }

		/// <summary>
		/// تاریخ صدور ( ارسال ) برگشت
		/// </summary>
		public string BouncedDate { get; set; } = string.Empty;

		/// <summary>
		/// دلایل برگشت
		/// </summary>
		public SamatChequeBouncedReasonDto BouncedReason { get; set; } = new();

		/// <summary>
		/// کد شعبه برگشت زننده
		/// </summary>
		public string BranchBounced { get; set; } = string.Empty;

		/// <summary>
		/// کد شعبه افتتاح کننده
		/// </summary>
		public string BranchOrigin { get; set; } = string.Empty;

		/// <summary>
		/// نحوه ارائه چک
		/// </summary>
		public int ChannelKind { get; set; }

		/// <summary>
		/// کد ارز
		/// </summary>
		public string CurrencyCode { get; set; } = string.Empty;

		/// <summary>
		/// نرخ ارز
		/// </summary>
		public decimal CurrencyRate { get; set; }

		/// <summary>
		/// تاریخ چک ( سررسید )
		/// </summary>
		public string DeadlineDate { get; set; } = string.Empty;

		/// <summary>
		/// شماره شباي حساب
		/// </summary>
		public string Iban { get; set; } = string.Empty;

		/// <summary>
		/// سریال چک
		/// </summary>
		public string Serial { get; set; } = string.Empty;

		/// <summary>
		/// نام شعبه برگشت زننده
		/// </summary>
		public string BouncedBranchName { get; set; } = string.Empty;

		/// <summary>
		/// نوع مشتری
		/// </summary>
		public int CustomerType { get; set; }

		/// <summary>
		/// کد رهگیري چک
		/// </summary>
		public int IdCheque { get; set; }

		/// <summary>
		/// نام شعبه افتتاح کننده
		/// </summary>
		public string OriginBranchName { get; set; } = string.Empty;
    }
}