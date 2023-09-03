using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.BlInterface.Publics;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Bl.PersonData
{
	/// <summary>
	/// اطلاعات تکمیلی
	/// </summary>
	internal sealed class RealPersonInfoCompletionBl : OpenAccountChainedRoBl<RealPerson, IRealPersonInfoCompletionRepository, Guid>, IRealPersonInfoCompletionBl
	{
		public RealPersonInfoCompletionBl(
			IRealPersonInfoCompletionRepository logicRepository,
			IRealPersonIdentificationBl preRequest,
			IHttpContextAccessor accessor,
			IRequestBl requestBl,
			ICityBl cityBl,
			IJobBl jobBl,
			IEducationBl educationBl,
			IPersonExceptionBl exceptionBl,
			IRequestStateLogBl requestLog) :base(logicRepository, preRequest, RequestStateType.PersonInfoCompletion, accessor, requestLog)
		{
			RequestBl = requestBl;
			CityBl = cityBl;
			JobBl = jobBl;
			EducationBl = educationBl;
			ExceptionBl = exceptionBl;
		}

		private readonly IRequestBl RequestBl;
		private readonly ICityBl CityBl;
		private readonly IJobBl JobBl;
		private readonly IEducationBl EducationBl;
		private readonly IPersonExceptionBl ExceptionBl;

		public override RequestStateType GetNextStep() => RequestStateType.PersonPostInquery;

		public override void Validate()
		{
			var realPerson = LogicRepository.GetRealPersonWithInfo(UserData.UserId).Result;
			if (realPerson == null || realPerson.RealPersonInfos == null || !realPerson.RealPersonInfos.Any())
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "اطلاعات پرسنلی یافت نشد"));

			if (!realPerson.RealPersonInfos.First().EducationId.HasValue || !realPerson.RealPersonInfos.First().JobId.HasValue || realPerson.CityId == 0)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "اطلاعات تکمیلی پرسنلی یافت نشد"));
		}

		/// <summary>
		/// بروزرسانی اطلاعات شهر - تحصیلات و شغل 
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public async Task PostRealPersonInfoCompletion(RealPersonInfoCompletionDto dto)
		{	//درخواست را بده
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();
			var realPerson = await LogicRepository.GetRealPersonWithInfo(UserData.UserId);
			if (realPerson == null || realPerson.RealPersonInfos == null || !realPerson.RealPersonInfos.Any())
				throw StException.DataNotFound("اطلاعات پرسنلی موجود نمی باشد");

			var city = await CityBl.Get(dto.CityId) ?? throw StException.DataNotFound("اطلاعات شهر معتبر نمی باشد");
			var job = await JobBl.Get(dto.JobId) ?? throw StException.DataNotFound("اطلاعات شغل معتبر نمی باشد");
			var education = await EducationBl.Get(dto.EducationId) ?? throw StException.DataNotFound("اطلاعات تحصیلات معتبر نمی باشد");

			realPerson.CityId = city.Id;
			realPerson.RealPersonInfos.First().EducationId = education.Id;
			realPerson.RealPersonInfos.First().JobId = job.Id;

			await LogicRepository.Update(realPerson, false);
			request = await GoToNextStep(request);// برو به مرحله ی بعد
			await RequestBl.Put(request);  // مرحله ی درخواست را بروز کن
		}

		public override async Task HandledExceptions(HttpStResult? result, Exception? exception)
		{
			var entity = new PersonException(RequestStateType.PersonInfoCompletion) { RequestId = RequestId };
			entity.SetException(exception);
			entity.SetMessageAndStatusCode(result);
			await ExceptionBl.Post(entity);
		}
	}
}