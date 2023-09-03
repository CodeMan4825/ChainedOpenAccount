using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace OpenAccount.Publics
{
	public static class Utility
	{
		/// <summary>
		/// Get description of enum.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetEnumDescription(this Enum value)
		{
			var fi = value.GetType().GetField(value.ToString());
			if (fi == null)
				return string.Empty;

			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes != null && attributes.Length > 0 ? attributes[0].Description : value.ToString();
		}

		/// <summary>
		/// تست صحت کد ملی
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool ValidateNationalCode(string input)
		{
			if (string.IsNullOrEmpty(input)) return false;
			if (input.Length != 10) return false;
			if (!long.TryParse(input, out _)) return false;

			byte count = 0;
			foreach (var item in input.Where(item => item == input[0]))
				count++;

			if (count == 10) return false;

			int result = 0, controlNr = input[9] - 48;
			for (var i = 0; i < input.Length - 1; i++)
				result += (input[i] - 48) * (10 - i);

			var remainder = result % 11;
			return controlNr == (remainder < 2 ? remainder : 11 - remainder);
		}

		/// <summary>
		/// تست صحت شناسه ملی افراد حقوقی
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool ValidateNationalId(string input)
		{
			if (string.IsNullOrEmpty(input)) return false;
			if (input.Length != 11) return false;
			if (!long.TryParse(input, out _)) return false;

			byte count = 0;
			foreach (var item in input.Where(item => item == input[0]))
				count++;

			if (count == 11) return false;

			int[] digits = { 29, 27, 23, 19, 17, 29, 27, 23, 19, 17 };

			int result = 0, controlNr = input[9] - 48 + 2;
			for (var i = 0; i < input.Length - 1; i++)
				result += (input[i] - 48 + controlNr) * digits[i];

			var remainder = result % controlNr;
			return remainder == 0 || input[10] - 48 == (remainder == 10 ? 0 : remainder);
		}

		/// <summary>
		/// Farsi day of DateTime.Now.
		/// </summary>
		/// <returns></returns>
		public static int FarsiNowDay() => new PersianCalendar().GetDayOfMonth(DateTime.Now);

		/// <summary>
		/// Get today's day of month
		/// </summary>
		/// <returns></returns>
		public static string FarsiNowDayStr() => $"{new PersianCalendar().GetDayOfMonth(DateTime.Now):00}";

		/// <summary>
		/// Farsi month of DateTime.Now.
		/// </summary>
		/// <returns></returns>
		public static string FarsiNowMonth() => $"{new PersianCalendar().GetMonth(DateTime.Now):00}";

		/// <summary>
		/// Farsi day of date.
		/// </summary>
		/// <returns></returns>
		public static int FarsiDay(DateTime dt) => new PersianCalendar().GetDayOfMonth(dt);

		/// <summary>
		/// Farsi month of date.
		/// </summary>
		/// <returns></returns>
		public static int FarsiMonth(DateTime dt) => new PersianCalendar().GetMonth(dt);

		/// <summary>
		/// Farsi year of dt.
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static int FarsiYear(DateTime dt) => new PersianCalendar().GetYear(dt);

		/// <summary>
		/// DateTime.Now => 20211008
		/// </summary>
		/// <returns></returns>
		public static string DateTimeNowToJustDate()
		{
			var dt = DateTime.Now;
			return $"{dt.Year:0000}{dt.Month:00}{dt.Day:00}";
		}

		/// <summary>
		/// DateTime.Now => 123005
		/// </summary>
		/// <returns></returns>
		public static string DateTimeNowToJustTime() => CastUtils.DateTimeToJustTime(DateTime.Now);

		/// <summary>
		/// Datetime.Now to Persian date.
		/// </summary>
		/// <returns></returns>
		public static int DateTimeNowToFarsi() => CastUtils.DateTimeToFarsi(DateTime.Now);

		/// <summary>
		/// Is number in the string ?
		/// </summary>
		/// <param name="str"></param>
		/// <returns>ABc2de => true</returns>
		public static bool IsNumberInStr(string str) => str.Any(s => byte.TryParse(s.ToString(), out _));

		/// <summary>
		/// Is string in the string ?
		/// </summary>
		/// <param name="str"></param>
		/// <returns>0912d654987 => true</returns>
		public static bool IsStrInNumber(string str) => str.Any(s => !byte.TryParse(s.ToString(), out _));

		public static string RandomString(int size, bool lowerCase)
		{
			var builder = new StringBuilder();
			var random = new Random();
			for (var i = 0; i < size; i++)
			{
				var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
				builder.Append(ch);
			}
			return lowerCase ? builder.ToString().ToLower() : builder.ToString();
		}
	}
}