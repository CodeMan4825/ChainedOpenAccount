namespace OpenAccount.Entities.Publics.SettingDto
{
	public sealed class DssSettingDto : SettingDto
	{
		public string PdfSign { get; set; } = string.Empty;
        public string PdfUserSign { get; set; } = string.Empty;
        public string PdfPutUserSign { get; set; } = string.Empty;
    }
}