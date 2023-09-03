using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Publics;
using System.ComponentModel;

namespace OpenAccount.Entities.PersonData
{
	/// <summary>
	/// اطلاعات تکمیلی شخص حقیقی
	/// </summary>
	[Description("اطلاعات تکمیلی شخص حقیقی")]
	public sealed class RealPersonInfo : PersonInfo
	{
		public RealPersonInfo()
		{
			IsDead = false;
			BirthPlaceAreaCode = string.Empty;
			BirthPlaceOfficeCode = string.Empty;
			SocialIdentityExtensionSeries = string.Empty;
			SocialIdentityNumber = 0;
			SocialIdentitySeries = 0;
		}

		/// <summary>
		/// شخص حقیقی
		/// </summary>
		[Comment("شخص حقیقی")]
        public Guid RealPersonid { get; set; }
        public RealPerson? RealPerson { get; set; }

        [Comment("شخص مرده است؟")]
		public bool IsDead { get; set; }

		/// <summary>
		/// محل تولد
		/// </summary>
		[Comment("محل تولد")]
		public string BirthPlaceAreaCode { get; set; } = string.Empty;

		/// <summary>
		/// محل صدور
		/// </summary>
		[Comment("محل صدور")]
		public string BirthPlaceOfficeCode { get; set; } = string.Empty;

		/// <summary>
		/// سری حرفی سریال شناسنامه
		/// </summary>
		[Comment("سری حرفی سریال شناسنامه")]
		public string SocialIdentityExtensionSeries { get; set; } = string.Empty;

		/// <summary>
		/// شماره شناسنامه
		/// </summary>
		[Comment("شماره شناسنامه")]
		public long SocialIdentityNumber { get; set; }

		/// <summary>
		/// سریال شناسنامه
		/// </summary>
		[Comment("سریال شناسنامه")]
		public int SocialIdentitySeries { get; set; }

        public Job? Job { get; set; }
		/// <summary>
		/// مشاغل
		/// </summary>
		[Comment("شغل")]
		public int? JobId { get; set; }

		/// <summary>
		/// تحصیلات
		/// </summary>
		public Education? Education { get; set; }

		/// <summary>
		/// تحصیلات
		/// </summary>
		[Comment("تحصیلات")]
		public byte? EducationId { get; set; } = 1;
    }
}