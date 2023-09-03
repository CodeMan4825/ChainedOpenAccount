namespace OpenAccount.Entities.Requests.InqueryCheque
{
	public sealed class SamatChequeBouncedReasonDto
	{
		public List<int> Int { get; set; } = new();

		/// <summary>
		/// get description of Int item.
		/// </summary>
		/// <param name="intValue">index of Int property.</param>
		/// <returns></returns>
		public static string GetReasonDesc(int intValue) => intValue switch
		{
			402 => "حساب داراي کسر موجودي است",
			403 => "حساب مورد نظر فاقد موجودي است",
			404 => "امضاء مطابقت ندارد",
			405 => "نقص امضاء دارد",
			406 => "مغایرت تاریخ عددي و حروفی",
			407 => "امضاء چک مخدوش است",
			408 => "مندرجات چک مخدوش است",
			409 => "مبلغ حروفی با عددي مغایر است",
			410 => "حساب مورد نظر بسته است",
			411 => "حساب مورد نظر مسدود است",
			412 => "چک با این سري و سریال مسدود است - حسب درخواست مشتري، ذینفع و یا مرجع قضایی مستند به ماده 14 قانون صدو چک",
			_ => string.Empty,
		};
	}
}