namespace OpenAccount.Entities.Publics.SettingDto
{
	public sealed class BtmsSettingDto : SettingDto
	{
		public string SamatChequeInquiry { get; set; } = string.Empty;
        public string SamatFacilityInquiry { get; set; } = string.Empty;
        public string PostalCodeInquiry { get; set; } = string.Empty;
        public string PostalCodeOfflineInquiry { get; set; } = string.Empty;		
		public string NacAllDataByNationalCode { get; set; } = string.Empty;
        public string OpenAccount { get; set; } = string.Empty;
        public string InsertSign { get; set; } = string.Empty;
    }
}