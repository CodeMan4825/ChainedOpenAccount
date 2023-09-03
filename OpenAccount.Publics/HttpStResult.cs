using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OpenAccount.Publics
{
	/// <summary>
	/// Simorq standard HttpResult.
	/// </summary>
	public class HttpStResult : ObjectResult
	{
		protected HttpStResult(object value, StStatusCodes statusCode) : base(value)
		{
			StatusCode = (int)statusCode;
		}

		[JsonIgnore]
		public string Message => Value == null ? string.Empty : Value.ToString();

        /// <summary>
        /// ورودی خالی پذیرفته نمی باشد
        /// </summary>
        /// <param name="data"></param>
        /// <returns><see cref="StStatusCodes.StArgumentNull"/></returns>
        public static HttpStResult ArgumentNull(object data) => new(data, StStatusCodes.StArgumentNull);

		/// <summary>
		/// داده در بازه ی نادرست پذیرفته نیست : message
		/// </summary>
		/// <param name="data"></param>
		/// <returns><see cref="StStatusCodes.RequestedRangeNotSatisfiable"/></returns>
		public static HttpStResult ArgumentOutOfRange(object data) => new(data, StStatusCodes.RequestedRangeNotSatisfiable);

		/// <summary>
		/// داده ای موجود نمی باشد
		/// </summary>
		/// <param name="data">داده</param>
		/// <returns><see cref="StStatusCodes.StDataNotFound"/></returns>
		public static HttpStResult DataNotFound(object data) => new(data, StStatusCodes.StDataNotFound);

		/// <summary>
		/// فایل پیدا نشد
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns><see cref="StStatusCodes.StFileNotFound"/></returns>
		public static HttpStResult FileNotFound(object fileName) => new(fileName, StStatusCodes.StFileNotFound);

		/// <summary>
		/// داده ای با این کلید یافت نشد
		/// </summary>
		/// <param name="data"></param>
		/// <returns><see cref="StStatusCodes.StKeyNotFound"/></returns>
		public static HttpStResult KeyNotFound(object data) => new(data, StStatusCodes.StKeyNotFound);

		/// <summary>
		/// سامانه مسیر را پیدا نمی کند
		/// </summary>
		/// <param name="data"></param>
		/// <returns><see cref="StStatusCodes.StDirectoryNotFound"/></returns>
		public static HttpStResult DirectoryNotFound(object data) => new(data, StStatusCodes.StDirectoryNotFound);

		/// <summary>
		/// دسترسی غیرمجاز
		/// </summary>
		/// <param name="data"></param>
		/// <returns><see cref="StStatusCodes.StAccessDenied"/></returns>
		public static HttpStResult AccessDenied(object data) => new(data, StStatusCodes.StAccessDenied);

		/// <summary>
		/// کاربر گرامی: "اطلاعات" تکراری می باشد
		/// </summary>
		/// <param name="data">اطلاعات</param>
		/// <returns><see cref="StStatusCodes.StDataDublicate"/></returns>
		public static HttpStResult DataDublicate(object data) => new(data, StStatusCodes.StDataDublicate);

		/// <summary>
		/// کاربر گرامی: "اطلاعات" نادرست می باشد
		/// </summary>
		/// <param name="data">اطلاعات</param>
		/// <returns><see cref="StStatusCodes.StIncorrectData"/></returns>
		public static HttpStResult IncorrectData(object data) => new(data, StStatusCodes.StIncorrectData);

		/// <summary>
		/// سرویس در دسترس نمی باشد : خطا
		/// </summary>
		/// <param name="exceptionMsg">خطا</param>
		/// <returns><see cref="StStatusCodes.ServiceUnavailable"/></returns>
		public static HttpStResult ServiceUnavailable(object exceptionMsg) => new(exceptionMsg, StStatusCodes.ServiceUnavailable);

		/// <summary>
		/// سامانه نتوانست نشانی را هماهنگ کند
		/// </summary>
		/// <returns></returns>
		/// <returns><see cref="StStatusCodes.StHttpCallInvalidOperationException"/></returns>
		public static HttpStResult HttpCallInvalidOperationException() => new(StMessages.HttpCallInvalidOperationException, StStatusCodes.StHttpCallInvalidOperationException);

		/// <summary>
		/// سامانه نتوانست درخواست را بفرستد : مشکل شبکه ,دسترسی یا دی ان اس
		/// </summary>
		/// <returns></returns>
		/// <returns><see cref="StStatusCodes.StHttpCallRequestException"/></returns>
		public static HttpStResult HttpCallRequestException() => new(StMessages.HttpCallRequestException, StStatusCodes.StHttpCallRequestException);

		/// <summary>
		/// فرستادن درخواست با درنگ زیاد انجام نشد
		/// </summary>
		/// <returns><see cref="StStatusCodes.StHttpCallTaskCanceledException"/></returns>
		public static HttpStResult HttpCallTaskCanceledException() => new(StMessages.HttpCallTaskCanceledException, StStatusCodes.StHttpCallTaskCanceledException);

		/// <summary>
		/// نشانی ناهمگون است
		/// </summary>
		/// <returns><see cref="StStatusCodes.StHttpCallUriFormatException"/></returns>
		public static HttpStResult HttpCallUriFormatException() => new(StMessages.HttpCallUriFormatException, StStatusCodes.StHttpCallUriFormatException);

		/// <summary>
		/// خطائی شناخته شده را سامانه مدیریت کرده است
		/// </summary>
		/// <returns><see cref="StStatusCodes.StManagedError"/></returns>
		public static HttpStResult ManagedError(object data) => new(data, StStatusCodes.StManagedError);

		/// <summary>
		/// خطائی در پیشرفت مراحل کار رخ داده است : خطا
		/// </summary>
		/// <param name="msg">عین خطائی که به کاربر نمایش داده می شود - بی کم و بیش</param>
		/// <returns><see cref="StStatusCodes.StChainOfRespLevelViolationError"/></returns>
		public static HttpStResult ChainOfRespLevelViolation(object msg) => new(msg, StStatusCodes.StChainOfRespLevelViolationError);

/*		/// <summary>
		/// خطائی در پیشرفت مراحل کار رخ داده است : اعتبار سنجی انجام نشده است
		/// </summary>
		/// <returns><see cref="StStatusCodes.StChainOfRespLevelViolationNoInquiryError"/></returns>
		public static HttpStResult<string> StChainOfRespLevelViolationNoInquiryError() => new(StMessages.ChainOfRespLevelViolationMessage, StStatusCodes.StChainOfRespLevelViolationNoInquiryError, "اعتبار سنجی انجام نشده است");*/

		/// <summary>
		/// نتیجه پذیرفته نمی باشد : خطا
		/// </summary>
		/// <param name="msg">خطا</param>
		/// <returns><see cref="StStatusCodes.StNotAcceptedResult"/></returns>
		public static HttpStResult ResultNotAcceptable(object msg) => new(msg, StStatusCodes.StNotAcceptedResult);

		/// <summary>
		/// خطا در ذخیره سازی اطلاعات : خطا
		/// </summary>
		/// <param name="msg">خطا</param>
		/// <returns><see cref="StStatusCodes.StSaveError"/></returns>
		internal static HttpStResult SaveError(object msg) => new(msg, StStatusCodes.StSaveError);

		/// <summary>
		/// نتیجه ی اعتبارسنجی پذیرفته نمی باشد
		/// </summary>
		/// <returns><see cref="StStatusCodes.StInquiryNotAcceptable"/></returns>
		internal static HttpStResult InquiryNotAcceptable() => new("نتیجه ی اعتبارسنجی پذیرفته نمی باشد", StStatusCodes.StInquiryNotAcceptable);
	}
}