using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace OpenAccount.Entities.PersonData
{
	/// <summary>
	/// شخص حقوقی
	/// </summary>
	[Description("شخص حقوقی")]
	public sealed class LegalPerson : Person
	{
		/// <summary>
		/// نام تجاری
		/// </summary>
		[Comment("نام تجاری")]
		public string Brand { get; set; } = string.Empty;
		
		/// <summary>
		/// شماره اقتصادی
		/// </summary>
		[Comment("شماره اقتصادی")]
		public string EconomicNo { get; set; } = string.Empty;
		
		/// <summary>
		/// شماره ثبت
		/// </summary>
		[Comment("شماره ثبت")]
		public string RegisterNo { get; set; } = string.Empty;
		
		/// <summary>
		/// نوع شرکت
		/// </summary>
		[Comment("نوع شرکت")]
		public CompanyType CompanyType { get; set; }

		/// <summary>
		/// اطلاعات تکمیلی شخص حقوقی
		/// </summary>
		public List<LegalPersonInfo>? LegalPersonInfos { get; set; }
	}
}