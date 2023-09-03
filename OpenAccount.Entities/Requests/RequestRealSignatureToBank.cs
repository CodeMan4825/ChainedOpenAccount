using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests
{
	/// <summary>
	/// ارسال پاراف خیس به بانک
	/// Id = Request.Id
	/// </summary>
	[Description("ارسال پاراف خیس به بانک")]
	public sealed class RequestRealSignatureToBank : BaseEntity<Guid>
	{
		/// <summary>
		/// درخواست
		/// </summary>
		public Request Request { get; set; } = new();

		/// <summary>
		/// آیا پاراف برای بانک بدرستی ارسال شد؟
		/// </summary>
		[Comment("آیا پاراف برای بانک بدرستی ارسال شد؟")]
		public bool SignatureSentToBank { get; set; } = false;

		/// <summary>
		/// نتیجه ی ارسال پاراف
		/// </summary>
		[Comment("نتیجه ی ارسال پاراف")]
		public string SendToBankMessage { get; set; } = string.Empty;

		/// <summary>
		/// زمان ارسال پاراف
		/// </summary>
		[Comment("زمان ارسال پاراف")]
		public DateTime SysDate { get; set; }

	}
}