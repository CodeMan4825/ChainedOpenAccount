using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.PersonData;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenAccount.Entities.Publics
{
	/// <summary>
	/// آدرس
	/// </summary>
	[Description("آدرس")]
	public sealed class Address : BaseEntity<Guid>
	{
		/// <summary>
		/// اشخاص
		/// </summary>
		[Comment("اشخاص")]
		[Column(Order = 1)]
		public Guid PersonId { get; set; }
		public Person Person { get; set; } = new();

		/// <summary>
		/// کدپستی
		/// </summary>
		[Comment("کدپستی")]
		[Column(Order = 2)]
		public string PostalCode { get; set; } = string.Empty;

		/// <summary>
		/// نام ساختمان
		/// </summary>
		[Comment("نام ساختمان")]
		public string BuildingName { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// طبقه
		/// </summary>
		[Comment("طبقه")]
		public string Floor { get; set; } = string.Empty;

		/// <summary>
		/// پلاک
		/// </summary>
		[Comment("پلاک")]
		public string HouseNumber { get; set; } = string.Empty;

		/// <summary>
		/// کد محله
		/// </summary>
		[Comment("کد محله")]
		public int? LocalityCode { get; set; }

		/// <summary>
		/// نام محله
		/// </summary>
		[Comment("نام محله")] 
		string LocalityName { get; set; } = string.Empty;

		/// <summary>
		/// نوع محله
		/// </summary>
		[Comment("نوع محله")] 
		public string LocalityType { get; set; } = string.Empty;

		/// <summary>
		/// استان
		/// </summary>
		[Comment("استان")] 
		public string Province { get; set; } = string.Empty;

		/// <summary>
		/// طبقه جانبی
		/// </summary>
		[Comment("طبقه جانبی")] 
		public string SideFloor { get; set; } = string.Empty;

		/// <summary>
		/// خیابان
		/// </summary>
		[Comment("خیابان")] 
		public string Street { get; set; } = string.Empty;

		/// <summary>
		/// خیابان
		/// </summary>
		[Comment("خیابان")] 
		public string Street2 { get; set; } = string.Empty;

		/// <summary>
		/// منطقه فرعی
		/// </summary>
		[Comment("منطقه فرعی")] 
		public string SubLocality { get; set; } = string.Empty;

		/// <summary>
		/// شهرستان
		/// </summary>
		[Comment("شهرستان")] 
		public string TownShip { get; set; } = string.Empty;

		/// <summary>
		/// روستا
		/// </summary>
		[Comment("روستا")] 
		public string Village { get; set; } = string.Empty;

		/// <summary>
		/// منطفه - ناحیه
		/// </summary>
		[Comment("منطفه - ناحیه")] 
		public string Zone { get; set; } = string.Empty;

		/// <summary>
		/// آدرس کامل
		/// </summary>
		[Comment("آدرس کامل")] 
		public string FullAddress { get; set; } = string.Empty;

		/// <summary>
		/// فعال است؟
		/// </summary>
		[Comment("فعال است؟")]
		public bool IsActive { get; set; }

		/// <summary>
		/// شماره موبایل
		/// </summary>
		[Comment("MobileNumber")]
		public string? MobileNumber { get; set; }

		/// <summary>
		/// تلفن ثابت
		/// </summary>
		[Comment("تلفن ثابت")]
		public string? Phone1 { get; set; }

		/// <summary>
		/// تلفن ثابت
		/// </summary>
		[Comment("تلفن ثابت")]
		public string? Phone2 { get; set; }

/*		/// <summary>
		/// Email
		/// </summary>
		[Comment("Email")]
		public string Email { get; set; } = string.Empty;
*/
        /// <summary>
        /// زمان درج
        /// </summary>
        [Comment("زمان درج")]
		public DateTime SysDate { get; set; }

		public string GetFullAddress => string.IsNullOrEmpty(FullAddress) ? $"{LocalityType} {TownShip} {SubLocality} {Street2} {Street}" : FullAddress;

		public override Address Clone() => (Address)MemberwiseClone();
	}

	public sealed class AddressToConfirmPostCodeDto
	{
		public string PostalCode { get; set; } = string.Empty;
		public string FullAddress { get; set; } = string.Empty;
	}
}