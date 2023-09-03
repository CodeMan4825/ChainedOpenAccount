using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Publics
{
	/// <summary>
	/// شهر
	/// </summary>
	[Description("شهر")]
    public sealed class City : BaseEntity<int>
	{
		[Comment("نام شهر")]
		public string Name { get; set; } = string.Empty;

		[Comment("استان")]
		public int ProvinceId { get; set; }
		public Province Province { get; set; } = new();
		public string PostCityCode { get; set; } = string.Empty;
    }
}
