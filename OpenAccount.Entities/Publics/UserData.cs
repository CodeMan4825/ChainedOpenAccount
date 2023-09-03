namespace OpenAccount.Entities.Publics
{
	public sealed class UserData
	{
		public Guid UserId { get; set; }
		public Guid ReferenceNumber { get; set; }
		public Guid OrganizationId { get; set; }
		public string UserName { get; set; } = string.Empty;
		public string TraceNumber { get; set; } = string.Empty;
		public string Ip { get; set; } = string.Empty;
		public string DeviceId { get; set; } = string.Empty;
		public string ClientId { get; set; } = string.Empty;
		public string Roles { get; set; } = string.Empty;
		public string Channel { get; set; } = string.Empty;
		public string NationalCode { get; set; } = string.Empty;
	}
}