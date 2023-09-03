using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests
{
	/// <summary>
	/// خطاهای امضای دیجیتال هر درخواست
	/// </summary>
	[Description("خطاهای امضای دیجیتال هر درخواست")]
	public sealed class RequestDigitalSignatureLog : BaseEntity<Guid>
	{
		/// <summary>
		/// امضای دیجیتال هر درخواست
		/// </summary>
		public RequestDigitalSignature RequestDigitalSignature { get; set; } = new();
		[Comment("امضای دیجیتال هر درخواست")]
        public Guid RequestDigitalSignatureId { get; set; }

		/// <summary>
		/// متن خطا
		/// </summary>
		[Comment("متن خطا")]
		public string ErrorMessage { get; set; } = string.Empty;

		/// <summary>
		/// زمان وقوع خطا
		/// </summary>
		[Comment("زمان وقوع خطا")]
		public DateTime SysDate { get; set; }
    }
}