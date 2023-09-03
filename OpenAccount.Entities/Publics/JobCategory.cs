using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Publics
{
	/// <summary>
	/// گروه مشاغل
	/// </summary>
	[Description("گروه مشاغل")]
	public sealed class JobCategory : BaseEntity<byte>
	{
        public string Name { get; set; } = string.Empty;
		public bool IsActive { get; set; }

        public ICollection<Job>? Jobs { get; set; }
    }
}