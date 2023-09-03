namespace OpenAccount.Entities.PersonData
{
	public sealed class IdentityDto
	{
		public string UserName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public bool IsMail => Gender.ToLower() == "male";
		public string LastName { get; set; } = string.Empty;
		public string NationalCode { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
	}
}