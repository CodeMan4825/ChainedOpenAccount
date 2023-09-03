using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests.InqueryCheque
{
	/// <summary>
	/// دلایل برگشت
	/// </summary>
	[Description("دلایل برگشت سمات")]
	public sealed class SamatChequeBouncedReason : BaseEntity<Guid>
	{
		/// <summary>
		/// Reason of bounce as int
		/// </summary>
		[Comment("Reason of bounce as int")]
		public int Int { get; set; }
		public Guid SamatBouncedChequeItemId { get; set; }

		/// <summary>
		/// Reason of bounce description
		/// </summary>
		[Comment("Reason of bounce description")]
		public string IntDescription
		{
			get => Int switch
				{
					402 => "حساب داراي کسر موجودي است",
					403 => "حساب مورد نظر فاقد موجودي است",
					404 => "امضاء مطابقت ندارد",
					405 => "نقص امضاء دارد",
					406 => "مغایرت تاریخ عددي و حروفی",
					407 => "امضاء چک مخدوش است",
					408 => "مندرجات چک مخدوش است",
					409 => "مبلغ حروفی با عددي مغایر است",
					410 => "حساب مورد نظر بسته است",
					411 => "حساب مورد نظر مسدود است",
					412 => "چک با این سري و سریال مسدود است",
					_ => "Unknown Reason",
				};
			private set { }
		}
	}
}