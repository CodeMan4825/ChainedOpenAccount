using System.Globalization;
using System.Net;

namespace OpenAccount.Publics
{
	/// <summary>
	/// Cast routins.
	/// </summary>
	public static class CastUtils
	{
		public static long StrToLong(string str) => long.TryParse(str.Trim(), out var result) ? result : default;

		public static bool StrToBool(string str) => bool.TryParse(str.Trim(), out var result) && result;

		public static int StrToInt(string str) => int.TryParse(str.Trim(), out var result) ? result : default;

		public static short StrToShort(string str) => short.TryParse(str.Trim(), out var result) ? result : default;

		public static byte StrToByte(string str) => byte.TryParse(str.Trim(), out var result) ? result : default;

		public static Guid StrToGuid(string str) => Guid.TryParse(str.Trim(), out var result) ? result : default;

		public static DateTime StrToDateTime(string str) => DateTime.TryParse(str.Trim(), out var result) ? result : default;

		/// <summary>
		/// Datetime to Persian date.
		/// </summary>
		/// <param name="dt"></param>
		/// <returns>Persian date like this : 13990101</returns>
		public static int DateTimeToFarsi(DateTime dt)
		{
			try
			{
				var pc = new PersianCalendar();
				var s = $"{pc.GetYear(dt):0000}{pc.GetMonth(dt):00}{pc.GetDayOfMonth(dt):00}";
				return int.Parse(s);
			}
			catch (Exception)
			{
				return 0;
			}
		}

		/// <summary>
		/// Datetime to Persian date.
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="delimeter"></param>
		/// <returns>Persian date like this : 1402/01/01</returns>
		public static string DateTimeToFarsiStr(DateTime dt, string delimeter = "/")
		{
			try
			{
				var pc = new PersianCalendar();
				return $"{pc.GetYear(dt):0000}{delimeter}{pc.GetMonth(dt):00}{delimeter}{pc.GetDayOfMonth(dt):00}";
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// DayOfWeek.Sunday => یکشنبه
		/// </summary>
		/// <param name="dt"></param>
		/// <returns>Farsi name of day of week.</returns>
		public static string GetFarsiDayOfWeekName(DateTime dt)
		{
			string[] arr = new string[7] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
			return arr[(byte)dt.DayOfWeek];
		}

		/// <summary>
		/// Convert farsi date to datetime
		/// </summary>
		/// <param name="fd">farsi date</param>
		/// <returns></returns>
		public static DateTime FarsiDateToDate(int fd)
		{
			var pc = new PersianCalendar();
			_ = int.TryParse(fd.ToString().AsSpan(0, 4), out var year);
			_ = int.TryParse(fd.ToString().AsSpan(4, 2), out var month);
			_ = int.TryParse(fd.ToString().AsSpan(6, 2), out var day);
			var now = DateTime.Now;
			if (!pc.IsLeapYear(year) && month == 12 && day > 29)
				day = 29;
			else if (pc.IsLeapYear(year) && month == 12 && day > 30)
				day = 30;
			return new DateTime(year, month, day, 0, 0, 0, pc);
		}

		/// <summary>
		/// Convert fasri date to datetime
		/// </summary>
		/// <param name="fd">farsi date</param>
		/// <param name="hour0">hour & min & second be zero ?</param>
		/// <returns></returns>
		public static DateTime FarsiDateToDateHour0(int fd)
		{
			var pc = new PersianCalendar();
			_ = int.TryParse(fd.ToString().AsSpan(0, 4), out var year);
			_ = int.TryParse(fd.ToString().AsSpan(4, 2), out var month);
			_ = int.TryParse(fd.ToString().AsSpan(6, 2), out var day);

			if (!pc.IsLeapYear(year) && month == 12 && day > 29)
				day = 29;
			else if (pc.IsLeapYear(year) && month == 12 && day > 30)
				day = 30;
			return new DateTime(year, month, day, 0, 0, 0, pc);
		}

		public static string DateTimeToJustTime(DateTime dt, string delim = "") => $"{dt.Hour:00}{delim}{dt.Minute:00}{delim}{dt.Second:00}";

		public static int DateToFarsiWithFixDay(DateTime dt, int fixDay)
		{
			var pc = new PersianCalendar();
			return int.Parse($"{pc.GetYear(dt):0000}{pc.GetMonth(dt):00}{fixDay:00}");
		}

		public static DateTime DateWithoutTime(DateTime dt) => new(dt.Year, dt.Month, dt.Day, 0, 0, 0);

		/// <summary>
		/// xxxx:xx:xx to timespan.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="yearIndex"></param>
		/// <returns></returns>
		public static TimeSpan StrToTimeSpan(string str, int yearIndex = 0)
		{
			var time = str.Trim().Split(':');
			if (time.Length > 0)
			{
				_ = int.TryParse(time[yearIndex].Trim(), out var hour);
				_ = int.TryParse(time[yearIndex + 1].Trim(), out var minutes);
				_ = int.TryParse(time[yearIndex + 2].Trim(), out var secounds);
				return new TimeSpan(hour, minutes, secounds);
			}
			return TimeSpan.Zero;
		}

		/// <summary>
		/// Change format to use in Textbox.Text with mask.
		/// </summary>
		/// <param name="ip">Like this : 192.168.1.10</param>
		/// <returns>192.168.001.010</returns>
		public static string IpToMaskedText(string ip)
		{
			var output = string.Empty;
			var parts = ip.Split('.');
			for (int i = 0; i < parts.Length; i++)
			{
				output += parts[i].PadLeft(3, '0');
				if (i != parts.Length - 1)
					output += ".";
			}
			return output;
		}

		/// <summary>
		/// Change format from Textbox.Text with mask to normal ip.
		/// </summary>
		/// <param name="ip">Like this : 192. 68.001.010</param>
		/// <param name="output">Like : 192.68.1.10</param>
		/// <returns></returns>
		public static bool IpMaskedToNormal(string ip, out string output)
		{
			output = string.Empty;
			var parts = ip.Replace(" ", "").Split('.');
			for (int i = 0; i < parts.Length; i++)
				if (int.TryParse(parts[i], out var intVal))
				{
					output += intVal.ToString();
					if (i != parts.Length - 1)
						output += ".";
				}
			return IPAddress.TryParse(output, out _);
		}
	}
}