using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests.InqueryCheque
{
	/// <summary>
	/// استعلام چک سمات
	/// </summary>
	[Description("استعلام چک سمات")]
	public sealed class SamatChequeInquiryRequest : BaseEntity<Guid>, ISamatChequeInquiryPersonInfo
	{
		/// <summary>
		/// درخواست افتتاح حساب
		/// </summary>
        [Comment("درخواست افتتاح حساب")]
		public Guid RequestId { get; set; }
		public Request Request { get; set; } = new();

        /// <summary>
        /// Response code of service.
        /// </summary>
        [Comment("Response code of service")]
		public string ActionCode { get; set; } = string.Empty;
		public List<SamatBouncedChequeItem> SamatBouncedChequeItems { get; set; } = new();
		public string NationalId { get; set; } = string.Empty;

		[Comment("1: haghighi 2: hoghughi 3: haghighi atba 4: hoghughi atba")]
		public int PersonType { get; set; } = 1;

		/// <summary>
		/// Inquiry responses ok.
		/// </summary>
        public bool ActionCodeOk => ActionCode == "00000";

        /// <summary>
        /// نوع شخص
        /// </summary>
        public string PersonTypeName => PersonType switch
		{
			1 => "حقیقی ایرانی",
			2 => "حقوقی ایرانی",
			3 => "حقیقی غیر ایرانی",
			4 => "حقوقی غیر ایرانی",
			_ => string.Empty,
		};

		/// <summary>
		/// خطای سیستمی
		/// </summary>
		[Comment("خطای سیستمی")]
		public string ErrorExMessage { get; set; } = string.Empty;

        /// <summary>
        /// زمان استعلام
        /// </summary>
        [Comment("زمان استعلام")]
        public DateTime SysDate { get; set; }
    }
}