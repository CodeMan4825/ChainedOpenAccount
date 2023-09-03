using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Publics
{
	/// <summary>
	/// استان
	/// </summary>
	[Description("استان")]
    public sealed class Province : BaseEntity<int>
    {
        [Comment("نام استان")]
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public ICollection<City>? Cities { get; set; }
    }
}