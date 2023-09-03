namespace OpenAccount.Entities.Publics.Wallets
{
	public sealed class WalletTransferResponseDto
	{
		/// <summary>
		/// در صورت انجام شدن تراکنش این فیلد به عنوان پاسخ به سرویس گیرنده ارائه داده می گردد
		/// به کار ما نمی آید
		/// </summary>
		public string HostRrn { get; set; } = string.Empty;
	}

	/// <summary>
	/// کسر از کیف پول جهت افتتاح حساب
	/// </summary>
	public sealed class WalletAccountTransferResponse
	{
		public string TransactionNumber { get; set; } = string.Empty;
	}
}