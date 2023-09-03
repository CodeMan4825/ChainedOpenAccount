namespace OpenAccount.Publics
{
	public static class OpenAccountUtility
	{
		/// <summary>
		/// محاسبه ی شماره شبا از روی شماره حساب
		/// </summary>
		/// <param name="accountNumber">شماره حساب</param>
		/// <returns>شماره شبا</returns>
		/// <exception cref="StException.ArgumentNull(string)">اگر شماره حساب خالی باشد</exception>
		/// <exception cref="StException.RequestedRangeNotSatisfiable(string)">اگر طول شماره حساب بیش از 19 باشد</exception>
		public static string CalcShebaNumber(string accountNumber)
		{
			if (string.IsNullOrWhiteSpace(accountNumber))
				throw StException.ArgumentNull("شماره حساب");
			if (accountNumber.Length > 19)
				throw StException.RequestedRangeNotSatisfiable("شماره حساب");

			var bban = $"018{accountNumber.PadLeft(19, '0')}";
			var cd = 98 - (decimal.Parse($"{bban}182700") % 97);
			return decimal.Parse($"{cd}{bban}").ToString("IR000000000000000000000000");
		}
	}
}