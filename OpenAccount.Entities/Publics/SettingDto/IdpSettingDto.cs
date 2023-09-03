using System.ComponentModel;

namespace OpenAccount.Entities.Publics.SettingDto
{
	public sealed class IdpSettingDto : SettingDto
	{
		public string BajetId { get; set; } = string.Empty;
		public string GetProfile { get; set; } = string.Empty;
	}
}