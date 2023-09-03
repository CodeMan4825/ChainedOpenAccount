namespace OpenAccount.Entities.Requests.InqueryCheque
{
	public sealed class AllInquiryResponseDto
	{
		public ChequeInquiryResponseDto ChequeInquiry { get; set; } = new();
        public LoanInquiryResponseDto LoanInquiry { get; set; } = new();
    }

	public sealed class ChequeInquiryResponseDto
	{
		public bool ActionCodeOk { get; set; }
		public string ErrorExMessage { get; set; } = string.Empty;
		public List<BouncedChequeItemInquiryResponseDto> BouncedCheques { get; set; } = new();
    }

	public sealed class BouncedChequeItemInquiryResponseDto
	{
		public int BankCode { get; set; }

		/// <summary>
		/// مبلغ برگشتی
		/// </summary>
		public int BouncedAmount { get; set; }

		/// <summary>
		/// تاریخ صدور (ارسال) برگشت
		/// </summary>
		public string BouncedDate { get; set; } = string.Empty;

		/// <summary>
		/// سریال چک
		/// </summary>
		public string Serial { get; set; } = string.Empty;

		/// <summary>
		/// نام شعبه برگشت زننده
		/// </summary>
		public string BouncedBranchName { get; set; } = string.Empty;
		public string Iban { get; set; } = string.Empty;
	}

	public sealed class LoanInquiryResponseDto
	{
		public bool ActionCodeOk { get; set; }
		public string ErrorExMessage { get; set; } = string.Empty;
		public List<LoanItemInquiryResponseDto> LoanItems { get; set; } = new();
    }

	public sealed class LoanItemInquiryResponseDto
	{
		public string BankCode { get; set; } = string.Empty;
		public string ShobeName { get; set; } = string.Empty;
		public string ShobeCode { get; set; } = string.Empty;
        public long AmountLated { get; set; }
        public int LastInstallmentDate { get; set; }
    }
}