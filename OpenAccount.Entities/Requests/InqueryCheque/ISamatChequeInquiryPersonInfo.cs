namespace OpenAccount.Entities.Requests.InqueryCheque
{
	public interface ISamatChequeInquiryPersonInfo
    {
		/// <summary>
		/// کد ملی
		/// </summary>
		string NationalId { get; set; }

		/// <summary>
		/// نوع شخص
		/// 1 = حقیقی ایرانی
		/// 2 = حقوقی ایرانی
		/// 3 = حقیقی غیر ایرانی
		/// 4 = حقوقی غیر ایرانی
		/// </summary>
		int PersonType { get; set; }
    }
}