using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Accounts
{
	/// <summary>
	/// لاگ افتتاح حساب کاربر
	/// Id = Request.Id
	/// </summary>
	[Description("لاگ افتتاح حساب کاربر")]
	public sealed class UserAccountLog : BaseEntity<Guid>
	{
		public UserAccount UserAccount { get; set; } = new();

        public string ActionMessage { get; set; } = string.Empty;
		public string ActionCode { get; set; } = string.Empty;
		public string? ErrorMessages { get; set; }
		public string TraceNumber { get; set; } = string.Empty;
		public string ReferenceNumber { get; set; } = string.Empty;
		[Comment("Data response code")]
		public int ResponseCode { get; set; } = 0;
		[Comment("Data response text")]
		public string ResponseText { get; set; } = string.Empty;
        public DateTime SysDate { get; set; }
    }
}