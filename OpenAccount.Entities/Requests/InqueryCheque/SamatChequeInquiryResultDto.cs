namespace OpenAccount.Entities.Requests.InqueryCheque
{
	/// <summary>
	/// لیست چکهاي برگشتی
	/// </summary>
	public sealed class SamatChequeInquiryResultDto
    {
		public List<SamatBouncedChequeItemDto> BouncedChequeCustomerModel { get; set; } = new();
    }
}