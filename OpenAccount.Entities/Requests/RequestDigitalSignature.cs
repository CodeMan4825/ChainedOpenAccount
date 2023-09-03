using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests
{
	/// <summary>
	/// امضای دیجیتال هر درخواست
	/// Id = Request.Id
	/// </summary>
	[Description("امضای دیجیتال هر درخواست")]
	public sealed class RequestDigitalSignature : BaseEntity<Guid>
	{
		/// <summary>
		/// درخواست
		/// </summary>
		public Request Request { get; set; } = new();

		/// <summary>
		/// فایل از روی داده های کاربر درست و بایگانی شد
		/// </summary>
		[Comment("فایل از روی داده های کاربر درست و بایگانی شد")]
		public bool PdfGenerated { get; set; }

		/// <summary>
		/// فایل تولید شده تا امضای طرفین
		/// </summary>
		[Comment("فایل تولید شده تا امضای طرفین")]
		public byte[]? TempPdfFile { get; set; }

		/// <summary>
		/// گواهی ریشه
		/// </summary>
		[Comment("گواهی ریشه")]
		public string RootCertification { get; set; } = string.Empty;

        /// <summary>
        /// گواهی نخست
        /// </summary>
        [Comment("گواهی نخست")]
		public string FirstDigest { get; set; } = string.Empty;

		/// <summary>
		/// گواهی تولید شده بوسیله ی برنامه جانبی
		/// </summary>
		[Comment("گواهی تولید شده بوسیله ی برنامه جانبی")]
		public string SignGeneratedByApp { get; set; } = string.Empty;

/*		/// <summary>
		/// گواهی امضاء دیجیتال کاربر ارسال شد؟
		/// </summary>
		[Comment("گواهی امضاء دیجیتال کاربر ارسال شد؟")]
		public bool SignatureSent { get; set; }
*/
		/// <summary>
		/// گواهی پایانی
		/// </summary>
		[Comment("گواهی پایانی")]
		public string FinalDigest { get; set; } = string.Empty;

        /// <summary>
        /// فایل بوسیله ی بانک امضاء شد؟
        /// </summary>
        [Comment("فایل بوسیله ی بانک امضاء شد؟")]
		public bool PdfSignedByBank { get; set; }

		/// <summary>
		/// تاریخ اعمال شدن امضاء
		/// </summary>
		[Comment("تاریخ اعمال شدن امضاء")]
		public DateTime SysDate { get; set; }

		/// <summary>
		/// نام فایل در سیستم مدیریت فایل
		/// </summary>
		[Comment("نام فایل در سیستم مدیریت فایل")]
		public string FileNameInDms { get; set; } = string.Empty;

		/// <summary>
		/// اطلاعات پرحجم و حساس را پاک کن
		/// </summary>
		public void RemoveUnusableData()
		{
			FinalDigest = string.Empty;
			FirstDigest = string.Empty;
			SignGeneratedByApp = string.Empty;
			RootCertification = string.Empty;
			TempPdfFile = null;
		}

		/// <summary>
		/// خطاهای امضای دیجیتال هر درخواست
		/// </summary>
		public ICollection<RequestDigitalSignatureLog>? RequestDigitalSignatureLogs { get; set; }
    }

	public sealed class RequestDigitalSignatureRequestDto
	{
		public string SignerCertificate { get; set; } = string.Empty;
    }

	public sealed class StatusOfRequestDigitalSignature
	{
		/// <summary>
		/// فایل از روی داده های کاربر درست و بایگانی شد
		/// </summary>
		[Comment("فایل از روی داده های کاربر درست و بایگانی شد")]
		public bool PdfGenerated { get; set; }

		/// <summary>
		/// فایل بوسیله ی بانک امضاء شد؟
		/// </summary>
		[Comment("فایل بوسیله ی بانک امضاء شد؟")]
		public bool PdfSignedByBank { get; set; }

		/// <summary>
		/// نام فایل در سیستم مدیریت فایل
		/// </summary>
		[Comment("نام فایل در سیستم مدیریت فایل")]
		public string FileNameInDms { get; set; } = string.Empty;
	}

	public sealed class GetRealSignatureFromUids
	{
		public string RealSignature { get; set; } = string.Empty;
    }
}