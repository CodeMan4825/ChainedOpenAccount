using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Publics.BtmsDtos;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Requests;
using OpenAccount.Entities.Publics.SettingDto;
using Microsoft.Extensions.Options;
using OpenAccount.Entities.Publics.Exceptions;

namespace OpenAccount.Bl.Requests
{
	/// <summary>
	/// ارسال پاراف خیس به بانک
	/// </summary>
	internal sealed class RequestRealSignatureToBankBl : OpenAccountChainedBl<RequestRealSignatureToBank, IRequestRealSignatureToBankRepository, Guid>, IRequestRealSignatureToBankBl
	{
		private readonly MinIoSettingDto MinioSetting;
		private readonly BtmsSettingDto BtmsSetting;
		private readonly UidsSettingDto Uids;
		private readonly IRequestBl RequestBl;

		public RequestRealSignatureToBankBl(IRequestRealSignatureToBankRepository logicRepository,
			IUserAccountBl preRequest,
			IOptions<BtmsSettingDto> btmsSetting,
			IOptions<MinIoSettingDto> minioSetting,
			IOptions<UidsSettingDto> uids,
			IHttpContextAccessor accessor,
			IRequestBl requestBl,
			IRequestStateLogBl requestLog) : base(logicRepository, preRequest, RequestStateType.RealSignatureToBank, accessor, requestLog)
		{
			BtmsSetting = btmsSetting.Value;
			RequestBl = requestBl;
			MinioSetting = minioSetting.Value;
			Uids = uids.Value;
		}

		public override RequestStateType GetNextStep() => RequestStateType.SendIBanToBank;

		/// <summary>
		/// ارسال امضای خیس به بانک
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public async Task SendToBank(Guid requestId)
		{
			var req = await RequestBl.Get(requestId) ?? throw StException.RequestIdNotFound();
			var data = await Get(requestId) ?? new RequestRealSignatureToBank { SysDate = DateTime.Now, Request = req };

			if (data.SignatureSentToBank)
				throw StException.DataDublicate("امضای کاربر از پیش در حساب تعریف شده است");

			var accountNumber = await RequestBl.GetAccountNumber(requestId);
			if (accountNumber == string.Empty)
				throw StException.DataNotFound("شماره حساب موجود نمی باشد");
			// امضای دیجیتال
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var digitData = await HttpClients.Get<HttpSimorghApiResponseDto<GetRealSignatureFromUids>>(client, Uids.MainUrl, Uids.GetRealSignature);
			if (digitData == null || digitData.Data == null)
				throw StException.ServiceUnavailable("عدم دریافت پاسخ مناسب از سرویس امضاء کاربر");
			if (!digitData.ActionCodeOk)
				throw StException.ServiceUnavailable(digitData.ActionMessage);
			if (string.IsNullOrEmpty(digitData.Data.RealSignature))
				throw StException.IncorrectData("عدم دریافت امضاء کاربر از سرویس اطلاعات امضاء");

			// امضاء را به حساب بانکی کاربر در بانک پیوست کن
			client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var postSign = await HttpClients.Post<HttpSimorghApiResponseDto<SanamInsertSignRequestDto>>(client,
				BtmsSetting.MainUrl,
				BtmsSetting.InsertSign,
				new
				{
					accountNumber,
					radif = 0,
					signFile = digitData.Data.RealSignature,
					nationalCode = UserData.NationalCode,
					embossStamp = true
				});

			if (postSign == null)
				data.SendToBankMessage = "عدم امکان پیوست امضاء به حساب بانکی";
			else if (!postSign.ActionCodeOk)
				data.SendToBankMessage = postSign.ActionMessage;

			data.SignatureSentToBank = postSign != null && postSign.ActionCodeOk;
			if (data.SignatureSentToBank)
				await GoToNextStep(req);

			data.Request = req;
			if (data.Id == Guid.Empty)
			{
				data.Id = requestId;
				await Post(data);
			}
			else
				await Put(data);

			if (!data.SignatureSentToBank)
				throw StException.ServiceUnavailable(data.SendToBankMessage);
		}

		public override void Validate()
		{
			var data = LogicRepository.AsQuery().FirstOrDefault(x => x.Id == RequestId && x.SignatureSentToBank);
			if (data == null)
				StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "ارسال امضای کاربر به بانک انجام نشده است"));
			else if (!string.IsNullOrEmpty(data.SendToBankMessage))
				StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, data.SendToBankMessage));
		}
	}
}