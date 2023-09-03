/*using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests
{
	/// <summary>
	/// بایگانی پاراف خیس
	/// Id = Request.Id
	/// </summary>
	[Description("بایگانی پاراف خیس")]
	public sealed class RequestRealSignature : BaseEntity<Guid>
	{
		/// <summary>
		/// درخواست
		/// </summary>
		public Request Request { get; set; } = new();

		/// <summary>
		/// آیا پاراف بدرستی بایگانی شد؟
		/// </summary>
		[Comment("آیا پاراف بدرستی بایگانی شد؟")]
		public bool SignatureArchived { get; set; } = false;

		/// <summary>
		/// Signature file name in minIo.
		/// </summary>
		[Comment("Signature file name in minIo")]
		public string SignatureFileName { get; set; } = string.Empty;

		/// <summary>
		/// نتیجه ی بایگانی
		/// </summary>
		[Comment("نتیجه ی بایگانی")]
		public string? ArchiveError { get; set; }

		/// <summary>
		/// زمان ارسال پاراف
		/// </summary>
		[Comment("زمان ارسال پاراف")]
		public DateTime SysDate { get; set; }
	}

	public sealed class RequestRealSignatureDto
	{
		/// <summary>
		/// تصویر پاراف
		/// </summary>
		public string SignatureInBase64 { get; set; } = string.Empty;
    }
}*/