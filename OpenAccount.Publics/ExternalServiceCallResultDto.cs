namespace OpenAccount.Publics
{
	public sealed record ExternalServiceCallResultDto(int StatusCode)
	{
		public string Error { get; set; } = string.Empty;
	}
}