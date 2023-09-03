using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Publics
{
	/// <summary>
	/// مشاغل
	/// </summary>
	[Description("مشاغل")]
	public sealed class Job : BaseEntity<int>
	{
		/// <summary>
		/// Tata Id
		/// </summary>
		[Comment("Tata Id")]
		public byte Code { get; set; }
        public byte JobCategoryId { get; set; }
		public JobCategory JobCategory { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}