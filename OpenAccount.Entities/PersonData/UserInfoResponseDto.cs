namespace OpenAccount.Entities.PersonData
{
	public sealed class UserInfoResponseDto
	{
		public string UserName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string InvitationCode { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
	}
}