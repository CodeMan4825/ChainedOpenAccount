namespace OpenAccount.Entities.PersonData
{
	public sealed class RealPersonIdentificationDto
	{
		public int BirthDate { get; set; }

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
	}
}