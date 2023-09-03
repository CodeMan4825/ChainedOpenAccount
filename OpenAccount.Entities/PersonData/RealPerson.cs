using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace OpenAccount.Entities.PersonData
{
	/// <summary>
	/// شخص حقیقی
	/// </summary>
	[Description("شخص حقیقی")]
	public sealed class RealPerson : Person
	{
		[Comment("نام خانوادگی")]
		public string Family { get; set; } = string.Empty;

		[Comment("نام پدر")]
		public string FatherName { get; set; } = string.Empty;

		/// <summary>
		/// جنسیت
		/// </summary>
		[Comment("جنسیت")]
		public bool IsMale { get; set; } = true;

		[Comment("نام خانوادگی لاتین")]
		public string LatinFamily { get; set; } = string.Empty;

		/// <summary>
		/// اطلاعات تکمیلی شخص حقیقی
		/// </summary>
		public List<RealPersonInfo>? RealPersonInfos { get; set; }
	}

	public sealed class RealPersonForDSignDto
	{
		public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
		public string NationalCode { get; set; } = string.Empty;
		public DateTime BirthDate { get; set; }
	}
}