using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Accounts;
using OpenAccount.Entities.Publics.SettingDto;
using Microsoft.Extensions.Options;

namespace OpenAccount.Bl.Accounts
{
	/// <summary>
	/// ارسال شبای حساب به بانک
	/// </summary>
	internal sealed class SendUserAccountIBanToBankBl : OpenAccountChainedBl<UserAccount, ISendUserAccountIBanToBankRepository, Guid>, ISendUserAccountIBanToBankBl
	{
		private readonly WalletSetttingDto WalletSetting;
		private readonly IdpSettingDto IdpSettting;
		private readonly IRequestBl RequestBl;

		public SendUserAccountIBanToBankBl(ISendUserAccountIBanToBankRepository logicRepository,
			IRequestRealSignatureToBankBl preRequest,
			IHttpContextAccessor accessor,
			IOptions<WalletSetttingDto> walletSetting,
			IOptions<IdpSettingDto> idpSettting,
			IRequestBl requestBl,
			IRequestStateLogBl requestLog) :
			base(logicRepository, preRequest, RequestStateType.SendIBanToBank, accessor, requestLog)
		{
			WalletSetting = walletSetting.Value;
			IdpSettting = idpSettting.Value;
			RequestBl = requestBl;
		}

		public override RequestStateType GetNextStep() => RequestStateType.TransferFeeToAccount;

		/// <summary>
		/// ارسال شبای حساب به بانک
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public async Task SendIBanToBank(Guid requestId)
		{
			var request = await RequestBl.Get(requestId) ?? throw StException.RequestIdNotFound();
			var data = await Get(requestId) ?? throw StException.DataNotFound("افتتاح حسابی هنوز انجام نشده است");
			if (!string.IsNullOrEmpty(data.ShebaNumber))
				throw StException.DataDublicate("شماره ی شبا از پیش به بانک ارسال شده است");

			var shebaNumber = OpenAccountUtility.CalcShebaNumber(data.AccountNumber);
			StException? stException = null;

			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var result = await HttpClients.Put<HttpSimorghApiResponseDto<string>>(
				client,
				$"{WalletSetting.MainUrl}:{WalletSetting.TransactionPort}",
				string.Format(WalletSetting.RegIBan, UserData.UserId, IdpSettting.BajetId, shebaNumber),
				new { },
				new Dictionary<string, string>()
				{
					{ "referenceNumber", UserData.ReferenceNumber.ToString() },
					{ "traceNumber", UserData.TraceNumber },
				});
			if (result == null)
				stException = StException.ServiceUnavailable("ارسال شبای حساب به بانک با خطا مواجه شد");
			else if (!result.ActionCodeOk)
				stException = StException.ServiceUnavailable(result.ActionMessage);

			if (stException == null)
			{
				data.Request = await GoToNextStep(request);//برو به مرحله ی بعد
				data.ShebaNumber = shebaNumber;
			}
			else
				data.UserAccountLogs = new List<UserAccountLog>
				{
					new UserAccountLog
					{
						ActionMessage = stException.Message,
						SysDate = DateTime.Now,
						TraceNumber = UserData.TraceNumber,
						ReferenceNumber = UserData.ReferenceNumber.ToString(),
						ActionCode = result != null ? result.ActionCode : string.Empty
					}
				};
			await Put(data);
			if (stException != null)
				throw stException;
		}

		public override async void Validate()
		{
			var result = await LogicRepository.Get(RequestId) ?? throw StException.DataNotFound("ارسال شبای حساب کاربر به بانک انجام نشده است");
			if (string.IsNullOrEmpty(result.ShebaNumber))
				throw StException.ChainOfRespLevelViolation("شماره ی شبا به بانک ارسال نشده است");
		}
	}
}