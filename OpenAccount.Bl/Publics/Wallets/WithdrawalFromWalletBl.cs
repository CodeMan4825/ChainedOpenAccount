using DevExpress.DataProcessing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.BlInterface.Publics.Wallets;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Publics.Wallets;

namespace OpenAccount.Bl.Publics.Wallets
{
	/// <summary>
	/// برداشت از کیف پول مشتری
	/// </summary>
	internal sealed class WithdrawalFromWalletBl : BaseLogic<WithdrawalFromWallet, IWithdrawalFromWalletRepository, Guid>, IWithdrawalFromWalletBl
	{
		private readonly WalletSetttingDto WalletSetting;
		private readonly IdpSettingDto IpdSetting;
		private readonly IRequestBl RequestBl;

		public WithdrawalFromWalletBl(IWithdrawalFromWalletRepository logicRepository,
			IHttpContextAccessor accessor,
			IOptions<WalletSetttingDto> walletSetting,
			IOptions<IdpSettingDto> ipdSetting,
			IRequestBl requestBl) : base(logicRepository, accessor)
		{
			RequestBl = requestBl;
			WalletSetting = walletSetting.Value;
			IpdSetting = ipdSetting.Value;
		}

		public override async Task Post(WithdrawalFromWallet entity, bool save = true)
		{
			var request = await RequestBl.Get(entity.RequestId);
			if (request == null || request.PersonId != UserData.UserId)
				throw StException.KeyNotFound("شناسه ی درخواست نامعتبر می باشد");

			entity.Request = request;
			await base.Post(entity, save);
		}

		/// <summary>
		/// آیا برداشت از کیف پول با نوع خاص انجام شده است؟
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="used">برداشت های استفاده شده = true</param>
		/// <param name="eventType">نوع تراکنش</param>
		public Task<bool> ControlWithdrawal(Guid requestId, bool used, EventType eventType) =>
			Task.FromResult(LogicRepository.AsQuery().Any(x => x.RequestId == requestId &&
														  x.EventType == ((int)eventType).ToString() &&
														  x.ActionCode == "00000" &&
														  x.Used == used));

		/// <summary>
		/// برداشت انجام شده از کیف پول مورد استفاده قرار گرفت
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="eventType">نوع تراکنش</param>
		/// <param name="save">If true, calls SaveChanges()</param>
		/// <returns></returns>
		public async Task MakeUsedWithdrawal(Guid requestId, EventType eventType, bool save = true)
		{
			var result = LogicRepository.AsQuery().Where(x => x.RequestId == requestId && x.EventType == ((int)eventType).ToString() && x.ActionCode == "00000" && !x.Used).FirstOrDefault();
			if (result == null)
				throw StException.DataNotFound($"برداشت از کیف پول با نوع {Utility.GetEnumDescription(eventType)}");

			result.Used = true;
			result.UseDate = DateTime.Now;
			await LogicRepository.Update(result, save);
		}

		/// <summary>
		/// برداشت از کیف پول
		/// </summary>
		/// <param name="chargeAmount">مبلغ جهت برداشت</param>
		/// <param name="eventType">بابت</param>
		/// <param name="requestId">شناسه ی درخواست</param>
		/// <returns></returns>
		public async Task Withdrawal(long chargeAmount, EventType eventType, Guid requestId)
		{
			StException? stException = null;
			var dto = new WalletTransferRequestDto
			{
				SourceAccount = UserData.UserId.ToString(),
				Amount = chargeAmount.ToString(),
				OrganUserId = IpdSetting.BajetId,
				EventType = ((int)eventType).ToString(),
				Description = Utility.GetEnumDescription(eventType),
				DeviceId = UserData.DeviceId,
				RequestDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
			};
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var result = await HttpClients.Post<HttpSimorghApiResponseDto<WalletTransferResponseDto>>(
				client,
				$"{WalletSetting.MainUrl}:{WalletSetting.TransactionPort}",
				WalletSetting.Transactions,
				dto,
				new Dictionary<string, string>()
				{
					{ "referenceNumber", UserData.ReferenceNumber.ToString() },
					{ "traceNumber", UserData.TraceNumber },
				});
			if (result == null || result.Data == null || !result.ActionCodeOk)
				stException = StException.ServiceUnavailable($"برداشت از کیف پول : {result?.ActionMessage}");
			
			await Post(new WithdrawalFromWallet
			{
				ActionCode = result == null ? "Unknown result" : result.ActionCode,
				ActionMessage = result == null ? "Unknown result" : result.ActionMessage,
				Amount = dto.Amount,
				Description = dto.Description,
				DeviceId = dto.DeviceId,
				EventType = dto.EventType,
				HostRrn = result == null || result.Data == null ? "Unknown result" : result.Data.HostRrn,
				Id = Guid.NewGuid(),
				OrganUserId = dto.OrganUserId,
				ReferenceNumber = UserData.ReferenceNumber.ToString(),
				RequestDate = dto.RequestDate,
				SourceAccount = dto.SourceAccount,
				TraceNumber = UserData.TraceNumber,
				Used = false,
				RequestId = requestId
			});

			if (stException != null)
				throw stException;
			if (result != null && result.ActionCode == "15015")
				throw StException.ResultNotAcceptable(result.ActionMessage);
		}

		/// <summary>
		/// انتقال از کیف پول به حساب اصلی
		/// </summary>
		/// <param name="chargeAmount">مبلغ جهت برداشت</param>
		/// <param name="request">درخواست</param>
		/// <param name="accountNumber">شماره حساب</param>
		/// <returns></returns>
		public async Task WithdrawalTransferToAccount(long chargeAmount, Entities.Requests.Request request, string accountNumber)
		{
			StException? stException = null;
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var result = await HttpClients.Post<HttpSimorghApiResponseDto<WalletAccountTransferResponse>>(
				client,
				$"{WalletSetting.MainUrl}:{WalletSetting.AccountTransferPort}",
				WalletSetting.AccountTransfer,
				new
				{
					Amount = chargeAmount,
					RequestDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
					Description = "مبلغ اولیه افتتاح حساب",
					DestName = UserData.UserName,
					PaymentId = accountNumber
				},
				new Dictionary<string, string>()
				{
					{ "referenceNumber", UserData.ReferenceNumber.ToString() },
					{ "traceNumber", UserData.TraceNumber },
				});
			if (result == null || result.Data == null)
				stException = StException.ServiceUnavailable("عدم دریافت پاسخ مناسب جهت برداشت از کیف پول");
			else if (!result.ActionCodeOk)
				stException = StException.ServiceUnavailable(result.ActionMessage);
			else if (result.ActionCode == "22000")
				stException = StException.ServiceUnavailable("نتیجه ی واریز نامشخص، کطفا جهت حصول اطمینان از واریز، با پشتیبانی تماس بگیرید");

			await Post(new WithdrawalFromWallet
			{
				ActionCode = result == null ? "Unknown result" : result.ActionCode,
				ActionMessage = result == null ? "Unknown result" : result.ActionMessage,
				Amount = chargeAmount.ToString(),
				Description = Utility.GetEnumDescription(EventType.OpenAccount),
				DeviceId = UserData.DeviceId,
				EventType = ((int)EventType.OpenAccount).ToString(),
				HostRrn = string.Empty,
				Id = Guid.NewGuid(),
				OrganUserId = IpdSetting.BajetId,
				ReferenceNumber = UserData.ReferenceNumber.ToString(),
				RequestDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
				SourceAccount = UserData.UserId.ToString(),
				TraceNumber = UserData.TraceNumber,
				Used = true,
				RequestId = request.Id,
				Request = stException == null ? null : request,//////////////
				DestinationAccount = accountNumber,
				UseDate = DateTime.Now
			});

			if (stException != null)
				throw stException;
		}

		/*/// <summary>
		/// برداشت کلی همه هزینه ها
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public async Task WithdrawalAllCosts(Entities.Requests.Request request)
		{
			var requestId = request.Id;

			//تنظیمات فعال درخواست را برمی گرداند
			var setting = await RequestBl.GetAccountTypeSetting(requestId);
			// بابت استعلام ثبت احوال باید مبلغی دریافت شود؟
			if (setting.IdentificationInquiry > 0)
				if (!await ControlWithdrawal(requestId, false, EventType.IdentityInquiry)) // آیا از پیش برداشت کرده ایم؟
					await Withdrawal(setting.MinBalance, EventType.IdentityInquiry, requestId); // اگر نه، برداشت کن
			// بابت استعلام کد پستی باید مبلغی دریافت شود؟
			if (setting.PostalCodeInquiry > 0)
				if (!await ControlWithdrawal(requestId, false, EventType.PostalCodeInquiry))
					await Withdrawal(setting.MinBalance, EventType.PostalCodeInquiry, requestId);
			// بابت تمبر مالیاتی باید مبلغی دریافت شود؟
			if (setting.Stamp > 0)
				if (!await ControlWithdrawal(requestId, false, EventType.StampInquiry))
					await Withdrawal(setting.MinBalance, EventType.StampInquiry, requestId);
			// بابت صدور کارت باید مبلغی دریافت شود؟
			if (setting.CardPrice > 0)
				if (!await ControlWithdrawal(requestId, false, EventType.CardPrice))
					await Withdrawal(setting.MinBalance, EventType.CardPrice, requestId);
			// بابت ارسال کارت باید مبلغی دریافت شود؟
			if (setting.CardSendPrice > 0)
				if (!await ControlWithdrawal(requestId, false, EventType.CardSendPrice))
					await Withdrawal(setting.MinBalance, EventType.CardSendPrice, requestId);
			// تا اینجا همه ی برداشت ها از کیف پول بدرستی انجام شد
			if (setting.IdentificationInquiry > 0) // برداشت انجام شده از کیف پول مورد استفاده قرار گیرد
				await MakeUsedWithdrawal(requestId, EventType.IdentityInquiry, false);

			if (setting.PostalCodeInquiry > 0)
				await MakeUsedWithdrawal(requestId, EventType.PostalCodeInquiry, false);

			if (setting.Stamp > 0)
				await MakeUsedWithdrawal(requestId, EventType.StampInquiry, false);

			if (setting.CardPrice > 0)
				await MakeUsedWithdrawal(requestId, EventType.CardPrice, false);

			if (setting.CardSendPrice > 0)
				await MakeUsedWithdrawal(requestId, EventType.CardSendPrice, false);

			await RequestBl.Put(request); // here we save all changes or save nothing.
		}
	*/}
}