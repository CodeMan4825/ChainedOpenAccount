namespace OpenAccount.Entities.Publics.SettingDto
{
	public sealed class MinIoSettingDto : SettingDto
	{
		public string Id { get; set; } = "2";
        public string Upload { get; set; } = string.Empty;
		public string ReUpload { get; set; } = string.Empty; 
		public string Download { get; set; } = string.Empty;
		public string DownloadPath { get; set; } = string.Empty;
    }
}