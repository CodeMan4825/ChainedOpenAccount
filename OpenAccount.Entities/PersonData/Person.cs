using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.Publics;
using System.ComponentModel;

namespace OpenAccount.Entities.PersonData
{
	/// <summary>
	/// اشخاص
	/// </summary>
	[Description("اشخاص")]
	public class Person : BaseEntity<Guid>
	{
		[Comment("نام شخص")]
		public string Name { get; set; } = string.Empty;

		[Comment("نام لاتین شخص")]
		public string LatinName { get; set; } = string.Empty;
		
		/// <summary>
		/// کد / شناسه ملی شخص حقیقی / حقوقی
		/// </summary>
		[Comment("کد / شناسه ملی شخص حقیقی / حقوقی")]
		public string NationalCode { get; set; } = string.Empty;

		/// <summary>
		/// شهر محل تولد / ثبت
		/// </summary>
		[Comment("شهر محل تولد / ثبت")]
		public int CityId { get; set; }
		public City? City { get; set; }

		/// <summary>
		/// تاریخ تولد / ثبت
		/// </summary>
		[Comment("تاریخ محل تولد/ثبت")]
		public DateTime Date { get; set; }

		public ICollection<Address>? Addresses { get; set; }
	}
}