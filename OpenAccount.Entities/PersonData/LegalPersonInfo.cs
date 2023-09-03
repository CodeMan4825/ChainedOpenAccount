using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace OpenAccount.Entities.PersonData
{
	/// <summary>
	/// اطلاعات تکمیلی شخص حقوقی
	/// </summary>
	[Description("اطلاعات تکمیلی شخص حقوقی")]
	public sealed class LegalPersonInfo : PersonInfo
	{
		/// <summary>
		/// شخص حقوقی
		/// </summary>
		[Comment("شخص حقوقی")]
		public Guid LegalPersonid { get; set; }
		public LegalPerson? LegalPerson { get; set; }
	}
}