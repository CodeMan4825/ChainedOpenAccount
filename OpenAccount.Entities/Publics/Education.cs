using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Publics
{
	/// <summary>
	/// تحصیلات
	/// </summary>
	[Description("تحصیلات")]
	public sealed class Education : BaseEntity<byte>
	{
		/// <summary>
		/// عنوان
		/// </summary>
		[Comment("عنوان")]
		public string Title { get; set; } = string.Empty;
    }
}