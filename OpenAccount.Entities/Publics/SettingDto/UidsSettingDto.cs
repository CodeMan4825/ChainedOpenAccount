namespace OpenAccount.Entities.Publics.SettingDto
{
	public sealed class UidsSettingDto : SettingDto
	{
        public string CheckSabt { get; set; } = string.Empty;
        public string GetPersonInfo { get; set; } = string.Empty;
        public string IsUserAlive { get; set; } = string.Empty;
        public string GetRealSignature { get; set; } = string.Empty;
    }
}