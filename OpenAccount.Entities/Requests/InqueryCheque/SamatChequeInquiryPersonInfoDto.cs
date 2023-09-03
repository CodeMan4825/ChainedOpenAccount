namespace OpenAccount.Entities.Requests.InqueryCheque
{
	public sealed class SamatChequeInquiryPersonInfoDto : ISamatChequeInquiryPersonInfo
	{
		/// <summary>
		/// نام
		/// </summary>
		public string FirstName { get; set; } = string.Empty;

		/// <summary>
		/// نام خانوادگی و یا نام شرکت
		/// </summary>
		public string LastName { get; set; } = string.Empty;

		/// <summary>
		/// کد ملی
		/// </summary>
		public string NationalId { get; set; } = string.Empty;

		/// <summary>
		/// نوع شخص
		/// </summary>
		public int PersonType { get; set; }
	}
}