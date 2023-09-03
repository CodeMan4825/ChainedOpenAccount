using Microsoft.AspNetCore.Http;
using OpenAccount.Bl.Infrastructure.ChainOfResp;
using OpenAccount.BlInterface;
using OpenAccount.BlInterface.Infrastructure.ChainOfResp;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.Bl
{
	/// <summary>
	/// <inheritdoc/>
	/// With RequestStateType as TEnum.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TRepository"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	internal abstract class OpenAccountChainedRoBl<TEntity, TRepository, TKey> : BaseChainOfRespRoLogic<TEntity, TRepository, TKey, RequestStateType>, IOpenAccountChainedRoBl<TEntity, TKey>
		where TEntity : IBaseEntity<TKey>
		where TRepository : IBaseRoRepository<TEntity, TKey>
		where TKey : struct
	{
		protected readonly IRequestStateLogBl RequestLog;

		protected OpenAccountChainedRoBl(TRepository logicRepository,
			IBaseChainOfRespBl<RequestStateType>? preRequest,
			RequestStateType logicType,
			IHttpContextAccessor accessor,
			IRequestStateLogBl requestLog) : base(logicRepository, preRequest, logicType, accessor)
		{
			RequestLog = requestLog;
		}

		/// <summary>
		/// شماره درخواست از هدر دریافت شده و برمی گردد
		/// </summary>
		/// <exception cref="StException.ArgumentNull(string)"
		protected Guid RequestId
		{
			get
			{
				var reqIdFromHdr = GetHeader("RequestId");
				if (!Guid.TryParse(reqIdFromHdr, out var reqId))
					throw StException.ArgumentNull("شناسه ی درخواست");
				return reqId;
			}
		}

		protected bool RequestIdExists()
		{
			try
			{
				return RequestId != Guid.Empty;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// برو به مرحله ی بعد
		/// </summary>
		/// <param name="request">درخواست همراه با لاگ هایش</param>
		/// <returns>request</returns>
		protected Task<Request> GoToNextStep(Request? request)
		{
			if (request == null)
				throw StException.KeyNotFound("شناسه ی درخواست نامعتبر می باشد");

			request.RequestStateLogs.Add(new RequestStateLog(LogicType));//مرحله ی جاری که انجام شده
			request.RequestStateType = GetNextStep();//مرحله ی بعدی

			return Task.FromResult(request);
		}

		protected override void CustomValidate()
		{
			//در اولین مرحله هستیم
			if (LogicType == RequestStateType.Start)
				return;

			// Can I find RequestId in header ?
			if (!RequestIdExists())
				throw StException.IncorrectData("شناسه کاربر");

			//آخرین مرحله ی درخواست را که گذرانده
			var log = RequestLog.GetLastStateOfRequest(RequestId).Result;
			//هیچ مرحله ای ثبت نشده
			if (log == null)
				throw StException.ChainOfRespLevelViolation(Utility.GetEnumDescription(LogicType));

			//اگر آخرین مرحله ی گذرانده شده، جلوتر از این مرحله باشد، کنترل را انجام بده
			if (log.RequestState > LogicType)
				base.CustomValidate();
			//اگر درخواست شما جلوتر از آخرین مرحله ی گذرانده شده بود
			else if (log.RequestState < LogicType)
				// اگر از مرحله ی جاری هم جلوتر بودید، خطای دسترسی خواهید دید
				if (((byte)log.RequestState + 1) < ((int)LogicType))
					throw StException.ChainOfRespLevelViolation(StMessages.AccessDeniedMessage);
		}
	}
}