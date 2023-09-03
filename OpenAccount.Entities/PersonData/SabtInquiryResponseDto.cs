namespace OpenAccount.Entities.PersonData
{
	public abstract class SabtBaseInquiryResponseDto
	{
		public string BirthPlaceAreaCode { get; set; } = string.Empty;

		public string BirthPlaceOfficeCode { get; set; } = string.Empty;

		public string FatherName { get; set; } = string.Empty;

		public string FirstName { get; set; } = string.Empty;

		public int Gender { get; set; }

		public string LastName { get; set; } = string.Empty;

		public string NationalCode { get; set; } = string.Empty;

		public string PostalCode { get; set; } = string.Empty;

		public string SocialIdentityExtensionSeries { get; set; } = string.Empty;

		public int SocialIdentityNumber { get; set; }

		public int SocialIdentitySeries { get; set; }
		public string FirstNameEN { get; set; } = string.Empty; 
		public string LastNameEN { get; set;} = string.Empty;
    }

	/// <summary>
	/// نتیجه ی استعلام ثبت احوال
	/// </summary>
	public sealed class SabtInquiryResponseDto : SabtBaseInquiryResponseDto
	{
		public string BirthDate { get; set; } = string.Empty;
	}

	/// <summary>
	/// نتیجه ی استعلام ثبت احوال
	/// Offline
	/// </summary>
	public sealed class SabtOfflineInquiryResponseDto : SabtBaseInquiryResponseDto
	{
		public int BirthDatePersian { get; set; }
	}

	public sealed record IdentityInquiryResult
	{
        public bool IsInquirySucceed { get; set; }
		public string Message { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public int Gender { get; set; }
		public string LastName { get; set; } = string.Empty;
		public string SocialIdentityExtensionSeries { get; set; } = string.Empty;
		public int SocialIdentityNumber { get; set; }
		public int SocialIdentitySeries { get; set; }
		public string PostalCode { get; set; } = string.Empty;
	}
}