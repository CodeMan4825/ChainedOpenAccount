using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Accounts;

namespace OpenAccount.Bl.Accounts
{
	/// <summary>
	/// وضعیت کیف پول
	/// </summary>
	internal sealed class WalletStatusBl : OpenAccountChainedBl<WalletStatus, IWalletStatusRepository, Guid>, IWalletStatusBl
	{
		private readonly IRequestBl RequestBl;
		private readonly IAccountTypeSettingBl SettingBl;
		private readonly WalletSetttingDto WalletSetting;
		private readonly IdpSettingDto IpdSetting;

		public WalletStatusBl(
			IWalletStatusRepository logicRepository,
			IRequestFacilityInqueryBl preRequest,
			IHttpContextAccessor accessor,
			IAccountTypeSettingBl settingBl,
			IRequestStateLogBl requestLog,
			IOptions<WalletSetttingDto> walletSetting,
			IOptions<IdpSettingDto> ipdSetting,
			IRequestBl requestBl) : base(logicRepository, preRequest, RequestStateType.WalletStatus, accessor, requestLog)
		{
			SettingBl = settingBl;
			IpdSetting = ipdSetting.Value;
			WalletSetting = walletSetting.Value;
			RequestBl = requestBl;
		}

		public override RequestStateType GetNextStep() => RequestStateType.PersonIdentification;

		/// <summary>
		/// موجودی کیف پول مشتری را برمی گرداند
		/// </summary>
		/// <returns></returns>
		public async Task<HttpSimorghApiResponseDto<WalletStatusResponseDto>> GetWalletStatus()
		{
			var result = await HttpClients.Get<HttpSimorghApiResponseDto<WalletStatusResponseDto>>(
				new HttpClient(),
				$"{WalletSetting.MainUrl}:{WalletSetting.StatusPort}",
				string.Format(WalletSetting.Status, UserData.UserId, IpdSetting.BajetId),
				new Dictionary<string, string>()
				{
					{ "referenceNumber", UserData.ReferenceNumber.ToString() },
					{ "traceNumber", UserData.TraceNumber },
				});
			if (result == null)
				throw StException.ServiceUnavailable("عدم دریافت پاسخ ار سرویس استعلام کیف پول");
			else if (result.Data == null)
				throw StException.ResultNotAcceptable("پاسخ استعلام کیف پول خالی می باشد");
			else if (!result.ActionCodeOk)
				throw StException.ResultNotAcceptable(result.ActionMessage);
			else if (result.Data.Status.ToUpper() != "ACTIVE") //BLOCKED || DEACTIVE
				throw StException.ResultNotAcceptable($"استعلام کیف پول : {result.Data.Status} : {result.Data.Description}");

			return result;
		}

		/// <summary>
		/// آخرین وضعیت کیف پول مشتری لاگین کرده را برای نوع تراکنش برمی گرداند
		/// </summary>
		/// <param name="eventType"></param>
		/// <returns></returns>
		public async Task<WalletStatusResponseBriefDto> GetWalletStatusForType(EventType eventType)
		{
			//درخواست را بده
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();
			var entity = new WalletStatus
			{
				Id = Guid.NewGuid(),
				Request = request,
				SysDate = DateTime.Now
			};
			StException? stException = null;
			var result = new HttpSimorghApiResponseDto<WalletStatusResponseDto>();
			try
			{   //موجودی کیف پول مشتری را برمی گرداند
				result = await GetWalletStatus();
			}
			catch (StException ex) { stException = ex; }
			catch (Exception) { stException = StException.ServiceUnavailable("عدم دریافت پاسخ ار سرویس استعلام کیف پول"); }

			entity.ActionMessage = stException == null ? string.Empty : stException.Message;
			entity.ActionCode = result == null ? string.Empty : result.ActionCode;

			//موجودی مورد نیاز این حساب را برمی گرداند
			var setting = await SettingBl.GetSettingByRequestId(RequestId);
			switch (eventType)
			{
				case EventType.PostalCodeInquiry: entity.NeededBalance = setting.PostalCodeInquiry; break;
				case EventType.CardPrice: entity.NeededBalance = setting.CardPrice + setting.CardSendPrice + setting.CardToAccount; break;
				case EventType.OpenAccount: entity.NeededBalance = setting.MinBalance; break;
				default: throw new NotImplementedException();
			}
			entity.Balance = result == null || result.Data == null ? 0 : result.Data.CurrentBalance;
			var RemainedCharge = entity.Balance - entity.NeededBalance;

			await Post(entity);

			if (stException != null)
				throw stException;

			return new WalletStatusResponseBriefDto
			{
				CurrentBalance = entity.Balance, // موجودی کیف پول
				Description = result.Data.Description,
				Status = result.Data.Status,
				NeededBalance = entity.NeededBalance, // مجموع هزینه ها
				Filing = entity.NeededBalance - setting.MinBalance,//تشکیل پرونده
				InitialBalance = setting.MinBalance,//موجودی اولیه
				NeededCharge = RemainedCharge < 0 ? -RemainedCharge : 0 // شارژ مورد نیاز
			};
		}

		/// <summary>
		/// آخرین وضعیت کیف پول مشتری را برای کل هزینه ها برمی گرداند
		/// </summary>
		/// <returns></returns>
		public async Task<WalletStatusResponseBriefDto> GetWalletStatusForTotalBalance()
		{   //درخواست را بده
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();
			var entity = new WalletStatus
			{
				Id = Guid.NewGuid(),
				Request = request,
				SysDate = DateTime.Now
			};
			StException? stException = null;
			var result = new HttpSimorghApiResponseDto<WalletStatusResponseDto>();
			try
			{   //موجودی کیف پول مشتری را برمی گرداند
				result = await GetWalletStatus();
			}
			catch (StException ex)
			{
				stException = ex;
			}
			catch (Exception)
			{
				stException = StException.ServiceUnavailable("عدم دریافت پاسخ ار سرویس استعلام کیف پول");
			}
			entity.ActionMessage = stException == null ? string.Empty : stException.Message;
			entity.ActionCode = result == null ? string.Empty : result.ActionCode;

			//تنظیمات این حساب را برمی گرداند
			var setting = await SettingBl.GetSettingByRequestId(RequestId);
			entity.NeededBalance = await SettingBl.CalcNeededBalance(setting);
			entity.Balance = result == null || result.Data == null ? 0 : result.Data.CurrentBalance;

			var RemainedCharge = entity.Balance - entity.NeededBalance;
			if (entity.ActionCodeOk && RemainedCharge >= 0)
				entity.Request = await GoToNextStep(request);

			await Post(entity);

			if (stException != null)
				throw stException;

			return new WalletStatusResponseBriefDto
			{
				CurrentBalance = entity.Balance, // موجودی کیف پول
				Description = result.Data.Description,
				Status = result.Data.Status,
				NeededBalance = entity.NeededBalance, // مجموع هزینه ها
				Filing = entity.NeededBalance - setting.MinBalance,//تشکیل پرونده
				InitialBalance = setting.MinBalance,//موجودی اولیه
				NeededCharge = RemainedCharge < 0 ? -RemainedCharge : 0 // شارژ مورد نیاز
			};
		}

		public override void Validate() { }
	}
}