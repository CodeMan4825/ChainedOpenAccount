using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Bl.PersonData
{
	/// <summary>
	/// اطلاعات هویتی
	/// </summary>
	internal sealed class RealPersonIdentificationBl : OpenAccountChainedRoBl<RealPerson, IRealPersonIdentificationRepository, Guid>, IRealPersonIdentificationBl
	{
		private readonly IRequestBl RequestBl;
		private readonly UidsSettingDto UidsSetting;
		private readonly IPersonExceptionBl ExceptionBl;

		public RealPersonIdentificationBl(
			IRealPersonIdentificationRepository logicRepository,
			IWalletStatusBl preRequest,
			IHttpContextAccessor accessor,
			IOptions<UidsSettingDto> uidsSetting,
			IRequestBl requestBl,
			IPersonExceptionBl exceptionBl,
			IRequestStateLogBl requestLog) : base(logicRepository, preRequest, RequestStateType.PersonIdentification, accessor, requestLog)
		{
			RequestBl = requestBl;
			ExceptionBl = exceptionBl;
			UidsSetting = uidsSetting.Value;
		}

		public override RequestStateType GetNextStep() => RequestStateType.PersonInfoCompletion;

		public override void Validate()
		{   // اطلاعات شخص جهت کنترل مشخصات هویتی
			var realPerson = LogicRepository.GetRealPersonWithInfo(UserData.UserId).Result;
			if (realPerson == null || realPerson.RealPersonInfos == null)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "اطلاعات پرسنلی یافت نشد"));
			if (realPerson.Date == DateTime.MinValue || string.IsNullOrEmpty(realPerson.Name) || string.IsNullOrEmpty(realPerson.Family))
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "اطلاعات پرسنلی کامل نمی باشد"));
			if (!realPerson.RealPersonInfos.Any(x => x.IsActive))
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "اطلاعات تکمیلی پرسنلی کامل نمی باشد"));
			var rp = realPerson.RealPersonInfos.Where(x => x.IsActive).First();
			if (rp.IsDead)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "کاربر در قید حیات نمی باشد"));
		}

		/// <summary>
		/// استعلام ثبت احوال
		/// Offline
		/// </summary>
		public async Task IdentityInquiry()
		{   //درخواست را بده
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();

			// استعلام ثبت احوال
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var inquiryData = await HttpClients.Get<HttpSimorghApiResponseDto<SabtInquiryResponseDto>>(
				client,
				UidsSetting.MainUrl,
				UidsSetting.GetPersonInfo) ?? throw StException.ServiceUnavailable("عدم دریافت پاسخ از سرویس استعلام ثبت احوال");

			var realPerson = await LogicRepository.GetRealPersonWithInfo(UserData.UserId) ?? throw StException.DataNotFound("اطلاعات پرسنلی");

			/// اطلاعات تکمیلی شخص اگر وجود داشت، غیرفعال کن تا جدیدتر ثبت شود
			realPerson.RealPersonInfos ??= new List<RealPersonInfo>();
			realPerson.RealPersonInfos.ForEach(x => { x.IsActive = false; });

			var personInfo = new RealPersonInfo
			{
				Id = Guid.NewGuid(),
				ErrorMessage = inquiryData.ActionMessage,
				ErrorCode = inquiryData.ActionCode,
				SysDate = DateTime.Now,
				IsActive = true
			};
			var data = inquiryData.Data;
			if (data != null)
			{
				realPerson.Date = DateTime.Parse(data.BirthDate);
				realPerson.FatherName = data.FatherName;
				realPerson.Name = data.FirstName;
				realPerson.IsMale = data.Gender == 1;
				realPerson.Family = data.LastName;
				realPerson.LatinName = data.FirstNameEN;
				realPerson.LatinFamily = data.LastNameEN;

				personInfo.PostalCode = data.PostalCode;
				personInfo.BirthPlaceAreaCode = data.BirthPlaceAreaCode;
				personInfo.BirthPlaceOfficeCode = data.BirthPlaceOfficeCode;
				personInfo.SocialIdentityExtensionSeries = data.SocialIdentityExtensionSeries;
				personInfo.SocialIdentityNumber = data.SocialIdentityNumber;
				personInfo.SocialIdentitySeries = data.SocialIdentitySeries;
			}
			realPerson.RealPersonInfos.Add(personInfo);
			// اطلاعات تکمیلی شخص را اضافه کن
			await LogicRepository.Update(realPerson);

			if (inquiryData.ActionCodeOk)
			{
				request = await GoToNextStep(request);// برو به مرحله ی بعد
				await RequestBl.Put(request);  // مرحله ی درخواست را بروز کن
			}

			if (inquiryData.Data == null)
			{
				if (!inquiryData.ActionCodeOk)
					throw StException.ResultNotAcceptable($"خطا در در یافت اطلاعات هویتی : {inquiryData.ActionMessage}");

				throw StException.ResultNotAcceptable("استعلام اطلاعات هویتی نتیجه ای در بر نداشت");
			}
		}

		/// <summary>
		/// آیا کاربر زنده است؟
		/// </summary>
		/// <returns></returns>
		/// <exception cref="StException.AccessDenied">دسترسی غیرمجاز - کاربر زنده نیست</exception>
		/// <exception cref="StException.ServiceUnavailable">احراز هویت</exception>
		/// <exception cref="StException.DataNotFound">اطلاعات پرسنلی</exception>
		/// <exception cref="StException.ResultNotAcceptable">service ActionCode</exception>
		public async Task IsUserAlive()
		{   //درخواست را بده
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();
			var rPerson = await Get(request.PersonId) ?? throw StException.DataNotFound("مشکل در دریافت اطلاعات کاربر");

			// استعلام ثبت احوال
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var result = await HttpClients.Get<HttpUidsApiResponseDto<bool>>(client, UidsSetting.MainUrl,
				string.Format(UidsSetting.IsUserAlive, CastUtils.DateTimeToFarsi(rPerson.Date), UserData.NationalCode)) ?? throw StException.ServiceUnavailable("عدم دریافت پاسخ از سرویس احراز هویت");

			if (!result.Data)
			{
				var realPerson = await LogicRepository.GetRealPersonWithInfo(UserData.UserId) ?? throw StException.DataNotFound("مشکل در دریافت اطلاعات کاربر");
				if (realPerson.RealPersonInfos == null || !realPerson.RealPersonInfos.Any())
					throw StException.DataNotFound("مشکل در دریافت اطلاعات تکمیلی کاربر");

				realPerson.RealPersonInfos.First().IsDead = true;
				await LogicRepository.Update(realPerson);

				throw StException.AccessDenied("کاربر در قید حیات نیست");
			}
			else if (!result.Result.ActionCodeOk)
				throw StException.ResultNotAcceptable(result.Result.ActionMessage);
		}

		public override async Task HandledExceptions(HttpStResult? result, Exception? exception)
		{
			if (result != null && (result.StatusCode == (int)StStatusCodes.StKeyNotFound || 
				result.StatusCode == (int)StStatusCodes.ServiceUnavailable ||
				result.StatusCode == (int)StStatusCodes.StDataNotFound))
			{
				var entity = new PersonException(RequestStateType.PersonIdentification) { RequestId = RequestId };
				entity.SetException(exception);
				entity.SetMessageAndStatusCode(result);
				await ExceptionBl.Post(entity);
			}
		}
	}
}