using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OpenAccount.Entities.Requests
{
	/// <summary>
	/// سفارش کارت
	/// </summary>
	[Description("سفارش کارت")]
	public sealed class RequestCard : BaseEntity<Guid>
	{
		[JsonIgnore]
		public Request Request { get; set; } = new();

		public string KeyCode { get; set; } = string.Empty;

		public string CardPro { get; set; } = string.Empty;

        public string TemplateName { get; set; } = string.Empty;

        public string Layout { get; set; } = string.Empty;

        public string TName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}