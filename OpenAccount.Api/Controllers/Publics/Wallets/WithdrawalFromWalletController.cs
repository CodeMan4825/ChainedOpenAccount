using Microsoft.AspNetCore.Mvc;
using OpenAccount.Api.Infrastructure;
using OpenAccount.BlInterface.Publics.Wallets;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers.Publics.Wallets
{
	/// <summary>
	/// برداشت از کیف پول مشتری
	/// </summary>
	public sealed class WithdrawalFromWalletController : ApplicationController<WithdrawalFromWallet, IWithdrawalFromWalletBl, Guid>
	{
		public WithdrawalFromWalletController(IConfiguration configuration, IHttpContextAccessor accessor, IWithdrawalFromWalletBl baseLogic, IRequestBl requestBl) :
			base(configuration, accessor, baseLogic) => RequestBl = requestBl;

		private readonly IRequestBl RequestBl;

		/// <summary>
		/// برداشت از کیف پول با نوع استعلام ثبت احوال
		/// </summary>
		/// <returns></returns>
		[HttpPost("WithdrawalIdentityInquiry/{requestId}")]
		public async Task WithdrawalIdentityInquiry(Guid requestId)
		{	// شماره ی درخواست و کاربری که درخواست را ایجاد کرده باید کنترل شود
			var request = await RequestBl.Get(requestId);
			if (request == null || request.PersonId != UserData.UserId)
				throw StException.KeyNotFound("شناسه ی درخواست نامعتبر می باشد");

			await ControllerLogic.Withdrawal((await RequestBl.GetAccountTypeSetting(requestId)).IdentificationInquiry, EventType.IdentityInquiry, requestId);
		}

		/// <summary>
		/// برداشت از کیف پول با نوع استعلام کدپستی
		/// </summary>
		/// <returns></returns>
		[HttpPost("WithdrawalPostalCodeInquiry/{requestId}")]
		public async Task WithdrawalPostalCodeInquiry(Guid requestId)
		{	// شماره ی درخواست و کاربری که درخواست را ایجاد کرده باید کنترل شود
			var request = await RequestBl.Get(requestId);
			if (request == null || request.PersonId != UserData.UserId)
				throw StException.KeyNotFound("شناسه ی درخواست نامعتبر می باشد");

			await ControllerLogic.Withdrawal((await RequestBl.GetAccountTypeSetting(requestId)).PostalCodeInquiry, EventType.PostalCodeInquiry, requestId);
		}

		/// <summary>
		/// برداشت از کیف پول با نوع تمبرمالیاتی
		/// </summary>
		/// <returns></returns>
		[HttpPost("WithdrawalStampInquiry/{requestId}")]
		public async Task WithdrawalStampInquiry(Guid requestId)
		{	// شماره ی درخواست و کاربری که درخواست را ایجاد کرده باید کنترل شود
			var request = await RequestBl.Get(requestId);
			if (request == null || request.PersonId != UserData.UserId)
				throw StException.KeyNotFound("شناسه ی درخواست نامعتبر می باشد");

			await ControllerLogic.Withdrawal((await RequestBl.GetAccountTypeSetting(requestId)).Stamp, EventType.StampInquiry, requestId);
		}

		/// <summary>
		/// برداشت از کیف پول با نوع صدور کارت
		/// </summary>
		/// <returns></returns>
		[HttpPost("WithdrawalCardPriceInquiry/{requestId}")]
		public async Task WithdrawalCardPriceInquiry(Guid requestId)
		{	// شماره ی درخواست و کاربری که درخواست را ایجاد کرده باید کنترل شود
			var request = await RequestBl.Get(requestId);
			if (request == null || request.PersonId != UserData.UserId)
				throw StException.KeyNotFound("شناسه ی درخواست نامعتبر می باشد");

			await ControllerLogic.Withdrawal((await RequestBl.GetAccountTypeSetting(requestId)).CardPrice, EventType.CardPrice, requestId);
		}

		/// <summary>
		/// برداشت از کیف پول با نوع ارسال کارت
		/// </summary>
		/// <returns></returns>
		[HttpPost("WithdrawalCardSendPriceInquiry/{requestId}")]
		public async Task WithdrawalCardSendPriceInquiry(Guid requestId)
		{	// شماره ی درخواست و کاربری که درخواست را ایجاد کرده باید کنترل شود
			var request = await RequestBl.Get(requestId);
			if (request == null || request.PersonId != UserData.UserId)
				throw StException.KeyNotFound("شناسه ی درخواست نامعتبر می باشد");

			await ControllerLogic.Withdrawal((await RequestBl.GetAccountTypeSetting(requestId)).CardSendPrice, EventType.CardSendPrice, requestId);
		}

		/// <summary>
		/// برداشت از کیف پول با نوع کارمزد اتصال کارت به حساب
		/// </summary>
		/// <returns></returns>
		[HttpPost("WithdrawalCardToAccountInquiry/{requestId}")]
		public async Task WithdrawalCardToAccountInquiry(Guid requestId)
		{	// شماره ی درخواست و کاربری که درخواست را ایجاد کرده باید کنترل شود
			var request = await RequestBl.Get(requestId);
			if (request == null || request.PersonId != UserData.UserId)
				throw StException.KeyNotFound("شناسه ی درخواست نامعتبر می باشد");

			await ControllerLogic.Withdrawal((await RequestBl.GetAccountTypeSetting(requestId)).CardToAccount, EventType.CardToAccount, requestId);
		}
	}
}