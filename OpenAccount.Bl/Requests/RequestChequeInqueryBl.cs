using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Requests;
using OpenAccount.Entities.Requests.InqueryCheque;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Bl.Requests
{
	internal sealed class RequestChequeInqueryBl : OpenAccountChainedBl<SamatChequeInquiryRequest, IRequestChequeInqueryRepository, Guid>, IRequestChequeInqueryBl
	{
		private readonly IRequestBl RequestBl;

		public RequestChequeInqueryBl(
			IRequestChequeInqueryRepository logicRepository,
			IRequestStartBl preRequest,
			IRequestBl request,
			IHttpContextAccessor accessor,
			IRequestStateLogBl requestLog) : base(logicRepository, preRequest, RequestStateType.None, accessor, requestLog) => RequestBl = request;

		/// <summary>
		/// آخرین استعلام چک یک درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>SamatChequeInquiryRequest</returns>
		public async Task<SamatChequeInquiryRequest?> GetLastInquiry(Guid requestId) => await LogicRepository.GetLastInquiry(requestId);

		public override RequestStateType GetNextStep() => throw StException.ChainOfRespLevelViolation("Programmer !");

		/// <summary>
		/// استعلام چک برای افراد حقیقی
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public async Task InqueryForRealPeron(HttpSimorghApiResponseDto<SamatChequeInquiryResponseDto> data)
		{
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();
			var chequeRequest = new SamatChequeInquiryRequest
			{
				Id = Guid.NewGuid(),
				Request = request,
				ActionCode = data.ActionCode,
				SysDate = DateTime.Now,
				ErrorExMessage = data.ActionMessage
			};

			var value = data.Data;
			if (value != null)
			{
				chequeRequest.PersonType = value.Person.PersonType;
				chequeRequest.NationalId = value.Person.NationalId;
				foreach (var chq in value.Cheques.BouncedChequeCustomerModel)
				{
					var id = Guid.NewGuid();
					chequeRequest.SamatBouncedChequeItems.Add(new SamatBouncedChequeItem
					{
						Id = id,
						Amount = chq.Amount,
						BankCode = chq.BankCode,
						BouncedAmount = chq.BouncedAmount,
						BouncedBranchName = chq.BouncedBranchName,
						BouncedDate = chq.BouncedDate,
						BouncedReasons = chq.BouncedReason.Int.Select(x => new SamatChequeBouncedReason
						{
							Int = x,
							SamatBouncedChequeItemId = id
						}).ToList(),
						BranchBounced = chq.BranchBounced,
						BranchOrigin = chq.BranchOrigin,
						ChannelKind = chq.ChannelKind,
						CurrencyCode = chq.CurrencyCode,
						CurrencyRate = chq.CurrencyRate,
						CustomerType = chq.CustomerType,
						DeadlineDate = chq.DeadlineDate,
						Iban = chq.Iban,
						IdCheque = chq.IdCheque,
						OriginBranchName = chq.OriginBranchName,
						Serial = chq.Serial,
					});
				}
			}
			await Post(chequeRequest);
		}

		public override void Validate()
		{	// آخرین مرحله ی درخواست را که گذرانده
			var log = RequestLog.GetLastStateOfRequest(RequestId).Result;
			// اگر آخرین مرحله، تسهیلات بود گذر کن
			if (log?.RequestState == RequestStateType.Start)
				return;
			// آخرین استعلام چک یک درخواست را برمی گرداند
			var result = GetLastInquiry(RequestId).Result;
			if (result == null)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "استعلام سنجی چک انجام نشده است"));
			if (!result.ActionCodeOk)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "نتیجه ی اعتبار سنجی چک پذیرفته نمی باشد"));
		}
	}
}