namespace OpenAccount.Entities.Publics.SettingDto
{
	public sealed class WalletSetttingDto : SettingDto
	{
        /// <summary>
        /// برای استعلام
        /// </summary>
        public string StatusPort { get; set; } = string.Empty;

		/// <summary>
		/// برای استعلام
		/// </summary>
		public string Status { get; set; } = string.Empty;

        /// <summary>
        /// برای برداشت
        /// </summary>
        public string TransactionPort { get; set; } = string.Empty;

		/// <summary>
		/// برای برداشت
		/// </summary>
		public string Transactions { get; set; } = string.Empty;

		/// <summary>
		/// برای ثبت شبای حساب
		/// </summary>
		public string RegIBan { get; set; } = string.Empty;

		/// <summary>
		/// کسر از کیف پول برای افتتاح حساب
		/// </summary>
		public string AccountTransferPort { get; set; } = string.Empty;

		/// <summary>
		/// کسر از کیف پول برای افتتاح حساب
		/// </summary>
		public string AccountTransfer { get; set; } = string.Empty;
    }
}