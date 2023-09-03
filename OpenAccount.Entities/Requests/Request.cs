using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.Entities.Requests.InqueryCheque;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests
{
	/// <summary>
	/// درخواست افتتاح حساب
	/// </summary>
	[Description("درخواست افتتاح حساب")]
	public class Request : BaseEntity<Guid>
	{
		[Comment("شخص")]
		public Guid PersonId { get; set; }
		public Person? Person { get; set; }// = new();

		/// <summary>
		/// نوع حساب
		/// </summary>
		[Comment("نوع حساب")]
		public AccountType AccountType { get; set; }

		/// <summary>
		/// آخرین وضعیت (مرحله) درخواست
		/// </summary>
		[Comment("آخرین وضعیت (مرحله) در خواست")]
		public RequestStateType RequestStateType { get; set; }

		/// <summary>
		/// آخرین وضعیت (مرحله) درخواست - توضیحات
		/// </summary>
		public string RequestStateTypeDescription { get => OpenAccount.Publics.Utility.GetEnumDescription(RequestStateType); }

		/// <summary>
		/// استعلام چک
		/// </summary>
		public List<SamatChequeInquiryRequest>? SamatChequeInquiryRequests { get; set; }

		/// <summary>
		/// لاگ تغییر وضعیت درخواست از مرحله ای به مرحله ی دیگر
		/// </summary>
		public List<RequestStateLog> RequestStateLogs { get; set; } = new();

		/// <summary>
		/// تنظیماتی که برای هر درخواست افتتاح حساب است
		/// </summary>
		public RequestAccountTypeSetting? RequestAccountTypeSetting { get; set; }

		/// <summary>
		/// تراکنش های مالی کیف پول یک درخواست
		/// </summary>
		public ICollection<WalletStatus>? WalletStatuses { get; set; }

		/// <summary>
		/// دریافت پاراف دیجیتال
		/// </summary>
		public RequestDigitalSignature? RequestDigitalSignature { get; set; }

		/// <summary>
		/// ارسال پاراف خیس به بانک
		/// </summary>
		public RequestRealSignatureToBank? RequestRealSignatureToBank { get; set; }

		/// <summary>
		/// افتتاح حساب کاربر
		/// </summary>
		public UserAccount? UserAccount { get; set; }

		/// <summary>
		/// خطا های مرحله ی اولیه
		/// </summary>
		public ICollection<StartException>? StartExceptions { get; set; }

		/// <summary>
		/// برداشت از کیف پول مشتری
		/// </summary>
		public ICollection<WithdrawalFromWallet>? WithdrawalFromWallets { get; set; }

		/// <summary>
		/// سفارش کارت
		/// </summary>
		public RequestCard? RequestCards { get; set; }
	}

	public sealed class AvalableRequestsDto
	{
        public Guid Id { get; set; }
        public AccountType AccountType { get; set; }
		public string AccountTypeDescription { get; set; } = string.Empty;
		public RequestStateType RequestStateType { get; set; }
		public string RequestStateTypeDescription { get; set; } = string.Empty;
		public string ShebaNumber { get; set; } = string.Empty;
		public string AccountNumber { get; set; } = string.Empty;
    }
}