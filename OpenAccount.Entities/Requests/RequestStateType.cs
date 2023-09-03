using System.ComponentModel;

namespace OpenAccount.Entities.Requests
{
	/// <summary>
	/// مراحل افتتاح حساب از نخست تا پایان
	/// </summary>
	public enum RequestStateType : byte
	{
		None = 0,

		/// <summary>
		/// شروع
		/// </summary>
		[Description("شروع")]
		Start,

		/// <summary>
		/// استعلام تسهیلات
		/// </summary>
		[Description("استعلام تسهیلات")]
		FacilityInquery,

		/// <summary>
		/// حداقل مانده حساب و افزایش موجودی در صورت نیاز
		/// </summary>
		[Description("حداقل مانده حساب و افزایش موجودی")]
		WalletStatus,

		/// <summary>
		/// شناسائی هویت
		/// </summary>
		[Description("شناسائی هویت")]
		PersonIdentification,

		/// <summary>
		/// تکمیل اطلاعات کاربری
		/// </summary>
		[Description("تکمیل اطلاعات کاربری")]
		PersonInfoCompletion,

		/// <summary>
		/// استعلام کدپستی
		/// </summary>
		[Description("استعلام کدپستی")]
		PersonPostInquery,

		/// <summary>
		/// سفارش کارت
		/// </summary>
		[Description("سفارش کارت")]
		CardOrder,

		/// <summary>
		/// امضای دیجیتال
		/// </summary>
		[Description("امضای دیجیتال")]
		DigitalSignature,

		/// <summary>
		/// افتتاح حساب - تولید شماره حساب
		/// </summary>
		[Description("افتتاح حساب")]
		UserAccount,

		/// <summary>
		/// ارسال پاراف خیس به بانک
		/// </summary>
		[Description("ارسال پاراف خیس به بانک")]
		RealSignatureToBank,

		/// <summary>
		/// ارسال شبا به بانک
		/// </summary>
		[Description("ارسال شبا به بانک")]
		SendIBanToBank,

		/// <summary>
		/// انتقال پول از کیف به حساب شخص
		/// </summary>
		[Description("انتقال پول از کیف به حساب شخص")]
		TransferFeeToAccount,

		/// <summary>
		/// پایان
		/// </summary>
		[Description("پایان")]
		Finished
	}
}