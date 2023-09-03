namespace OpenAccount.Publics
{
	/// <summary>
	/// Simorgh exception.
	/// </summary>
	public class StException : Exception
	{
		protected StException(HttpStResult stResult) : base(stResult.Value.ToString()) => HttpResult = stResult;

		/// <summary>
		/// 
		/// </summary>
		public HttpStResult HttpResult { get; init; }

		/// <summary>
		/// ورودی خالی پذیرفته نمی باشد : فیلد خالی
		/// </summary>
		/// <param name="data">فیلد خالی</param>
		/// <remarks>data is Nullable</remarks>
		/// <returns><see cref="StStatusCodes.StArgumentNull"/></returns>
		public static StException ArgumentNull(object data) => new(HttpStResult.ArgumentNull(data));

		/// <summary>
		/// داده در بازه ی نادرست پذیرفته نیست : داده
		/// </summary>
		/// <param name="data">داده</param>
		/// <remarks>data is not Nullable</remarks>
		/// <returns><see cref="StStatusCodes.RequestedRangeNotSatisfiable"/></returns>
		public static StException RequestedRangeNotSatisfiable(object data) => new(HttpStResult.ArgumentOutOfRange(data));

		/// <summary>
		/// کاربر گرامی: "اطلاعات" نادرست می باشد
		/// </summary>
		/// <param name="data">اطلاعات</param>
		/// <remarks>data is not Nullable</remarks>
		/// <returns><see cref="StStatusCodes.StIncorrectData"/></returns>
		public static StException IncorrectData(object data) => new(HttpStResult.IncorrectData(data));

		/// <summary>
		/// کاربر گرامی: اطلاعات تکراری می باشد
		/// </summary>
		/// <param name="data">اطلاعات</param>
		/// <remarks>data is not Nullable</remarks>
		/// <returns><see cref="StStatusCodes.StDataDublicate"/></returns>
		public static StException DataDublicate(object data) => new(HttpStResult.DataDublicate(data));

		/// <summary>
		/// داده ای موجود نمی باشد
		/// </summary>
		/// <param name="data">داده</param>
		/// <returns><see cref="StStatusCodes.StDataNotFound"/></returns>
		public static StException DataNotFound(object data) => new(HttpStResult.DataNotFound(data));

		/// <summary>
		/// داده ای با این کلید یافت نشد
		/// </summary>
		/// <returns><see cref="StStatusCodes.StKeyNotFound"/></returns>
		public static StException KeyNotFound(object data) => new(HttpStResult.KeyNotFound(data));

		/// <summary>
		/// شناسه ی درخواست نامعتبر می باشد
		/// </summary>
		/// <returns></returns>
		public static StException RequestIdNotFound() => KeyNotFound("شناسه ی درخواست نامعتبر می باشد");

		/// <summary>
		/// دسترسی غیرمجاز
		/// </summary>
		/// <returns><see cref="StStatusCodes.StAccessDenied"/></returns>
		public static StException AccessDenied(object data) => new(HttpStResult.AccessDenied(data));

		/// <summary>
		/// فایل پیدا نشد
		/// </summary>
		/// <param name="fileName">نام فایل</param>
		/// <remarks>fileName is not Nullable</remarks>
		/// <returns><see cref="StStatusCodes.StFileNotFound"/></returns>
		public static StException FileNotFound(object fileName) => new(HttpStResult.FileNotFound(fileName));

		/// <summary>
		/// سامانه مسیر را پیدا نمی کند
		/// </summary>
		/// <returns><see cref="StStatusCodes.StDirectoryNotFound"/></returns>
		public static StException DirectoryNotFound(object data) => new(HttpStResult.DirectoryNotFound(data));

		/// <summary>
		/// سرویس در دسترس نمی باشد
		/// </summary>
		/// <param name="exceptionMsg">خطا</param>
		/// <returns><see cref="StStatusCodes.ServiceUnavailable"/></returns>
		public static StException ServiceUnavailable(object exceptionMsg) => new(HttpStResult.ServiceUnavailable(exceptionMsg));

		/// <summary>
		/// خطائی در پیشرفت مراحل کار رخ داده است
		/// </summary>
		/// <param name="data">خطا</param>
		/// <returns><see cref="StStatusCodes.StChainOfRespLevelViolationError"/></returns>
		public static StException ChainOfRespLevelViolation(object data) => new(HttpStResult.ChainOfRespLevelViolation(data));

/*		/// <summary>
		/// خطائی در پیشرفت مراحل کار رخ داده است : اعتبار سنجی انجام نشده است
		/// </summary>
		/// <param name="data">خطا</param>
		/// <returns><see cref="StStatusCodes.StChainOfRespLevelViolationNoInquiryError"/></returns>
		public static StExceptionMain<string> ChainOfRespLevelViolationNoInquiry() => new(HttpStResult<string>.StChainOfRespLevelViolationNoInquiryError());*/

		/// <summary>
		/// نتیجه پذیرفته نمی باشد
		/// </summary>
		/// <param name="msg">خطا</param>
		/// <returns><see cref="StStatusCodes.StNotAcceptedResult"/></returns>
		public static StException ResultNotAcceptable(object data) => new(HttpStResult.ResultNotAcceptable(data));

		/// <summary>
		/// خطا در ذخیره سازی اطلاعات
		/// </summary>
		/// <param name="msg">خطا</param>
		/// <returns><see cref="StStatusCodes.StSaveError"/></returns>
		public static StException SaveError(object data) => new(HttpStResult.SaveError(data));

		/// <summary>
		/// نتیجه ی اعتبارسنجی پذیرفته نمی باشد
		/// </summary>
		/// <returns><see cref="StStatusCodes.StInquiryNotAcceptable"/></returns>
		public static StException InquiryNotAcceptable() => new(HttpStResult.InquiryNotAcceptable());
	}
}