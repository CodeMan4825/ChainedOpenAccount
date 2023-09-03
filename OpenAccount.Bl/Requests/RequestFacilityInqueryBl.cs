using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Entities.Requests;
using OpenAccount.Entities.Requests.InqueryCheque;
using OpenAccount.Entities.Requests.InqueryLoan;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Bl.Requests
{
	/// <summary>
	/// استعلام تسهیلات
	/// </summary>
	internal sealed class RequestFacilityInqueryBl : OpenAccountChainedBl<SamatLoanInquiryRequest, IRequestFacilityInqueryRepository, Guid>, IRequestFacilityInqueryBl
	{
		private readonly BtmsSettingDto BtmsSetting;
		private readonly IRequestBl RequestBl;
		private readonly IInquiryExceptionBl ExceptionBl;

		public RequestFacilityInqueryBl(
			IRequestFacilityInqueryRepository logicRepository,
			IRequestChequeInqueryBl preRequest,
			IHttpContextAccessor accessor,
			IOptions<BtmsSettingDto> btmsSetting,
			IInquiryExceptionBl exceptionBl,
			IRequestStateLogBl requestLog,
			IRequestBl requestBl) : base(logicRepository, preRequest, RequestStateType.FacilityInquery, accessor, requestLog)
		{
			BtmsSetting = btmsSetting.Value;
			ExceptionBl = exceptionBl;
			RequestBl = requestBl;
		}

		/// <summary>
		/// آخرین استعلام تسهیلات یک درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>SamatLoanInquiryRequest</returns>
		public async Task<SamatLoanInquiryRequest?> GetLastInquiry(Guid requestId) => await LogicRepository.GetLastInquiry(requestId);

		public override RequestStateType GetNextStep() => RequestStateType.WalletStatus;

		/// <summary>
		/// استعلام تسهیلات و چک برای افراد حقیقی
		/// </summary>
		/// <returns>تسهیلات و چک مشکل دار</returns>
		public async Task<AllInquiryResponseDto> InqueryForRealPerson()
		{   // استعلام چک برای افراد حقیقی
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var result = await HttpClients.Post<HttpSimorghApiResponseDto<SamatChequeInquiryResponseDto>>(client,
				BtmsSetting.MainUrl,
				BtmsSetting.SamatChequeInquiry,
				new
				{
					requestInfo = RequestId,
					personInfo = new
					{
						nationalId = UserData.NationalCode,
						personType = 1
					}
				});
			await (PreRequest as IRequestChequeInqueryBl).InqueryForRealPeron(result);
			var chequeResult = (result.ActionCodeOk || result.ActionCode == "41032"/*chq453*/) && result.Data != null && result.Data.IsValid;
			var chequeData = await (PreRequest as IRequestChequeInqueryBl).GetLastInquiry(RequestId);

			client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var data = await HttpClients.Get<HttpSimorghApiResponseDto<SamatLoanInquiryResponseDto>>(client, BtmsSetting.MainUrl,
				string.Format(BtmsSetting.SamatFacilityInquiry, Guid.NewGuid(), UserData.NationalCode));

			var inquiryResult = data != null && data.ActionCodeOk && data.Data != null && !data.Data.HasError && data.Data.ReturnValue != null;
			
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound(); //درخواست را بده
			if (inquiryResult && chequeResult) // اگر نتیجه ی استعلام ها مثبت بود برو به مرحله ی بعد
			{   // آخرین مرحله ی درخواست را که گذرانده
				var log = await RequestLog.GetLastStateOfRequest(RequestId);
				// اگر از مرحله ی امضای دیجیتال آمده بود، مرحله ی بعد نمی رود
				if (log?.RequestState <= RequestStateType.FacilityInquery)
					_ = await GoToNextStep(request);//برو به مرحله ی بعد
			}

			var entity = data == null || data.Data == null || data.Data.ReturnValue == null ? new SamatLoanInquiryRequest() : data.Data.ReturnValue;
			entity.Id = Guid.NewGuid();
			entity.Request = request;
			entity.ActionCode = data.ActionCode;
			entity.ErrorExMessage = data.ActionMessage;
			entity.SysDate = DateTime.Now;
			await LogicRepository.Add(entity);

			return MakeResponseOfInquiry(chequeData ?? new SamatChequeInquiryRequest(), entity);
		}

		private static AllInquiryResponseDto MakeResponseOfInquiry(SamatChequeInquiryRequest cheque, SamatLoanInquiryRequest loan)
		{
			var chequeData = new ChequeInquiryResponseDto
			{
				ActionCodeOk = cheque.ActionCodeOk,
				ErrorExMessage = cheque.ErrorExMessage,
				BouncedCheques = cheque.SamatBouncedChequeItems.Select(x => new BouncedChequeItemInquiryResponseDto
				{
					BankCode = x.BankCode,
					BouncedAmount = x.BouncedAmount,
					BouncedDate = x.BouncedDate,
					Serial = x.Serial,
					BouncedBranchName = x.BouncedBranchName,
					Iban = x.Iban
				}).ToList()
			};
			var loanData = new LoanInquiryResponseDto
			{
				ActionCodeOk = loan.ActionCodeIsValid,
				ErrorExMessage = loan.ErrorExMessage,
				LoanItems = loan.EstelamAsliRows.Select(x => new LoanItemInquiryResponseDto
				{
					BankCode = x.BankCode,
					ShobeName = x.ShobeName,
					ShobeCode = x.ShobeCode,
					AmountLated = CastUtils.StrToLong(x.AmMashkuk) + CastUtils.StrToLong(x.AmSarResid) + CastUtils.StrToLong(x.AmMoavagh),
					LastInstallmentDate = CastUtils.StrToInt(x.DateSarResid)
				}).ToList()
			};
			return new AllInquiryResponseDto { ChequeInquiry = chequeData, LoanInquiry = loanData };
		}

		public override void Validate()
		{	// آخرین استعلام تسهیلات یک درخواست را برمی گرداند
			var result = GetLastInquiry(RequestId).Result;
			if (result == null)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "استعلام تسهیلات انجام نشده است"));
			if (!result.ActionCodeIsValid)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "نتیجه ی اعتبار سنجی تسهیلات پذیرفته نمی باشد"));
		}

		public override async Task HandledExceptions(HttpStResult? result, Exception? exception)
		{
			if (result == null || result.StatusCode != (int)StStatusCodes.StManagedError)
				return;

			var entity = new InquiryException() { RequestId = RequestId };
			entity.SetException(exception);
			entity.SetMessageAndStatusCode(result);
			await ExceptionBl.Post(entity);
		}
	}
}