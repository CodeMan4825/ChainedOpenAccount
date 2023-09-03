using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.BlInterface.Publics.Wallets;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Bl.Requests
{
	/// <summary>
	/// سفارش کارت
	/// </summary>
	internal sealed class RequestCardBl : OpenAccountChainedBl<RequestCard, IRequestCardRepository, Guid>, IRequestCardBl
	{
		private readonly IRequestBl RequestBl;
		private readonly CardSettingDto CardSetting;
		private readonly IWithdrawalFromWalletBl WalletBl;
		private readonly IWalletStatusBl StatusBl;

		public RequestCardBl(IRequestCardRepository logicRepository,
			IRealPersonPostInqueryBl preRequest,
			IRequestBl request,
			IHttpContextAccessor accessor,
			IOptions<CardSettingDto> options,
			IWithdrawalFromWalletBl walletBl,
			IWalletStatusBl statusBl,
			IRequestStateLogBl requestLog) : base(logicRepository, preRequest, RequestStateType.CardOrder, accessor, requestLog)
		{
			RequestBl = request;
			WalletBl = walletBl;
			StatusBl = statusBl;
			CardSetting = options.Value;
		}

		public override RequestStateType GetNextStep() => RequestStateType.DigitalSignature;

		/// <summary>
		/// لیست کارت های موجود را از سرویس کارت برگردان
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<RequestCard>> GetCardsFromService()
		{
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var data = await HttpClients.Get<HttpSimorghCardApiResponseDto<IEnumerable<RequestCard>>>(client, CardSetting.MainUrl, CardSetting.Debit);

			if (data == null || !data.ActionCodeOk || data.Data == null || !data.Data.Any())
				throw StException.ServiceUnavailable($"مدل کارت {(!string.IsNullOrEmpty(data?.Message) ? data.Message : string.Empty)}");

			return data.Data;
		}

		public override async Task Post(RequestCard entity, bool save = true)
		{
			var requestId = RequestId;
			//تنظیمات فعال درخواست را برمی گرداند
			var setting = await RequestBl.GetAccountTypeSetting(requestId);
			var currentBalance = await StatusBl.GetWalletStatusForType(EventType.CardToAccount);

			var request = await RequestBl.Get(requestId) ?? throw StException.KeyNotFound("شناسه ی درخواست معتبر نمی باشد");

			// بابت صدور کارت باید مبلغی دریافت شود؟ اگر بله، قبلا دریافت نشده است؟
			var cardPriceMustGet = setting.CardPrice > 0 && !await WalletBl.ControlWithdrawal(requestId, false, EventType.CardPrice);
			// بابت ارسال کارت باید مبلغی دریافت شود؟ اگر بله، قبلا دریافت نشده است؟
			var cardSendPriceMustGet = setting.CardSendPrice > 0 && !await WalletBl.ControlWithdrawal(requestId, false, EventType.CardSendPrice);
			// بابت اتصال کارت به حساب باید مبلغی دریافت شود؟ اگر بله، قبلا دریافت نشده است؟
			var cardToAccountMustGet = setting.CardToAccount > 0 && !await WalletBl.ControlWithdrawal(requestId, false, EventType.CardToAccount);

			if (currentBalance.NeededCharge == 0)
			{   // کم کردن کیف پول
				if (cardPriceMustGet) await WalletBl.Withdrawal(setting.CardPrice, EventType.CardPrice, requestId);
				if (cardSendPriceMustGet) await WalletBl.Withdrawal(setting.CardSendPrice, EventType.CardSendPrice, requestId);
				if (cardToAccountMustGet) await WalletBl.Withdrawal(setting.CardToAccount, EventType.CardToAccount, requestId);
			}
			else
				throw StException.ResultNotAcceptable(new ValidateBalanceExceptionDto(LogicType, "کمبود موجودی", currentBalance.NeededCharge));

			// پول های کم شده ، همه با هم مورد استفاده قرار می گیرند
			if (setting.CardPrice > 0) await WalletBl.MakeUsedWithdrawal(requestId, EventType.CardPrice, false);
			if (setting.CardSendPrice > 0) await WalletBl.MakeUsedWithdrawal(requestId, EventType.CardSendPrice, false);
			if (setting.CardToAccount > 0) await WalletBl.MakeUsedWithdrawal(requestId, EventType.CardToAccount, false);

			entity.Request = await GoToNextStep(request);
			await base.Post(entity, save);
			//_ = await GoToNextStep(request);
			//entity.Id = request.Id;
			//await RequestBl.Put(request, true);
		}

		public override async Task Put(RequestCard entity, bool save = true)
		{
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();
			entity.Request = await GoToNextStep(request);
			await base.Put(entity, save);
		}

		public override void Validate()
		{
			if (!LogicRepository.AsQuery().Any(x => x.Id == RequestId && x.IsActive))
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "کاربر کارتی را انتخاب نکرده است"));

			//تنظیمات فعال درخواست را برمی گرداند
			var setting = RequestBl.GetAccountTypeSetting(RequestId).Result;
			if (setting.CardPrice > 0 && !WalletBl.ControlWithdrawal(RequestId, true, EventType.CardPrice).Result)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "برداشت از کیف پول جهت صدور کارت انجام نشده است"));
			// بابت ارسال کارت باید مبلغی دریافت شود؟
			if (setting.CardSendPrice > 0 && !WalletBl.ControlWithdrawal(RequestId, true, EventType.CardSendPrice).Result)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, $"برداشت از کیف پول جهت ارسال کارت انجام نشده است"));
			// بابت اتصال کارت به حساب باید مبلغی دریافت شود؟
			if (setting.CardToAccount > 0 && !WalletBl.ControlWithdrawal(RequestId, true, EventType.CardToAccount).Result)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, $"برداشت از کیف پول جهت اتصال کارت به حساب انجام نشده است"));
		}
	}
}