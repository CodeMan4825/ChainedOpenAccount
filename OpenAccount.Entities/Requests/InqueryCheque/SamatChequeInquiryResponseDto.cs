namespace OpenAccount.Entities.Requests.InqueryCheque
{
	public sealed class SamatChequeInquiryResponseDto
    {
        public SamatChequeInquiryResultDto Cheques { get; set; } = new();

		/// <summary>
		/// لیست خطاها
		/// </summary>
		public List<Error>? ErrorList { get; set; }

		/// <summary>
		/// اطلاعات شخص
		/// </summary>
		public SamatChequeInquiryPersonInfoDto Person { get; set; } = new();

		/// <summary>
		/// اعتبار عملیات
		/// </summary>
		public bool IsValid { get; set; }

		/// <summary>
		/// کد رهگیري درخواست
		/// </summary>
		public string RequestId { get; set; } = string.Empty;

		/// <summary>
		/// زمان درخواست
		/// </summary>
		public DateTime RequestDateTime { get; set; }
	}

	public sealed class Error
	{
		/// <summary>
		/// کد خطا
		/// </summary>
		public string ErrorCode { get; set; } = string.Empty;

		/// <summary>
		/// فیلد مربوطه
		/// </summary>
		public string Field { get; set; } = string.Empty;

    }
}