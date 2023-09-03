using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Bl.Requests
{
	/// <summary>
	/// Create Request entity.
	/// </summary>
	internal sealed class RequestStartBl : OpenAccountChainedBl<Request, IRequestStartRepository, Guid>, IRequestStartBl
	{
		private readonly IEntityExceptionBl EntityExceptionBl;
		private readonly IStartExceptionBl StartExceptionBl;
		private readonly IRealPersonBl RealPersonBl;
		private readonly IdpSettingDto Idp;
		private readonly UidsSettingDto Uids;
		private readonly IAccountTypeSettingBl SettingBl;

		public RequestStartBl(IRequestStartRepository logicRepository,
			IHttpContextAccessor accessor,
			IAccountTypeSettingBl settingBl,
			IRealPersonBl realPersonBl,
			IOptions<IdpSettingDto> idp,
			IOptions<UidsSettingDto> uids,
			IRequestStateLogBl requestLog,
			IEntityExceptionBl entityExceptionBl,
			IStartExceptionBl startExceptionBl) : base(logicRepository, null, RequestStateType.Start, accessor, requestLog)
		{
			SettingBl = settingBl;
			RealPersonBl = realPersonBl;
			Idp = idp.Value;
			Uids = uids.Value;
			EntityExceptionBl = entityExceptionBl;
			StartExceptionBl = startExceptionBl;
		}

		public override RequestStateType GetNextStep() => RequestStateType.FacilityInquery;

		/// <summary>
		/// درج درخواست با نوع خاص
		/// </summary>
		/// <param name="accountType"></param>
		/// <returns></returns>
		public async Task PostForRealPesron(AccountType accountType)
		{
			if (accountType == AccountType.ProfitAccount)
				throw StException.AccessDenied("سیستم مرابحه در حال حاضر غیرفعال می باشد");
			// امضای دیجیتال
			await CheckSignExisis();

			//هر شخص از هر نوع حساب یکی می تواند داشته باشد
			if (LogicRepository.AsQuery().Any(x => x.PersonId == UserData.UserId && x.AccountType == accountType))
				throw StException.DataDublicate($"نوع حساب {Utility.GetEnumDescription(accountType)} تکراری می باشد");

			var setting = await SettingBl.GetActiveSettingsByType(accountType) ?? throw StException.DataNotFound("اطلاعات تنظیمات حساب در دسترس نمی باشد");

			// استعلام ثبت احوال
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var inquiryData = await HttpClients.Get<HttpSimorghApiResponseDto<SabtOfflineInquiryResponseDto>>(client, Uids.MainUrl, Uids.GetPersonInfo);

			if (inquiryData == null || inquiryData.Data == null)
				throw StException.ServiceUnavailable("عدم دریافت پاسخ از سرویس استعلام ثبت احوال");

			var age = CastUtils.FarsiDateToDate(inquiryData.Data.BirthDatePersian);
			var now = DateTime.Now;
			if (age < now.AddYears(-setting.MaxAge) || age > now.AddYears(-setting.MinAge))
				throw StException.RequestedRangeNotSatisfiable($"سن از {setting.MinAge} تا {setting.MaxAge} پذیرفته می باشد");

			// آیا شخص وجود دارد؟
			var person = await RealPersonBl.Get(UserData.UserId);
			if (person == null)
			{
				client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
				var result = await HttpClients.Get<HttpSimorghApiResponseDto<IdentityDto>>(client, Idp.MainUrl, Idp.GetProfile);
				if (result == null || result.Data == null || !result.ActionCodeOk)
					throw StException.DataNotFound($"عدم دسترسی به اطلاعات کاربر : {result?.ActionMessage}");
				person = new RealPerson
				{
					Id = UserData.UserId,
					NationalCode = UserData.NationalCode,
					Name = result.Data.FirstName,
					Date = DateTime.MinValue,
					CityId = 0,
					Family = result.Data.LastName,
					IsMale = result.Data.IsMail,
					FatherName = inquiryData.Data.FatherName,
					Addresses = new List<Address> { new() { MobileNumber = result.Data.PhoneNumber, } }
				};
			}
			var request = new Request()
			{
				Id = Guid.NewGuid(),
				AccountType = accountType,
				//RequestStateType = GetNextStep(),//مرحله ی بعدی
				Person = person
			};
			await GoToNextStep(request);
			//request.RequestStateLogs.Add(new(LogicType));//مرحله ی جاری که انجام شده
			request.RequestAccountTypeSetting = new RequestAccountTypeSetting { AccountTypeSettingId = setting.Id };
			await Post(request);
		}

		private async Task CheckSignExisis()
		{
			// امضای دیجیتال
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var digitData = await HttpClients.Get<HttpSimorghApiResponseDto<GetRealSignatureFromUids>>(client, Uids.MainUrl, Uids.GetRealSignature);
			if (digitData == null || digitData.Data == null)
				throw StException.ServiceUnavailable("عدم دسترسی به سرویس استعلام امضاء کاربر");
			if (!digitData.ActionCodeOk)
				throw StException.ResultNotAcceptable(digitData.ActionMessage);
			if (string.IsNullOrEmpty(digitData.Data.RealSignature))
				throw StException.IncorrectData("اطلاعات امضاء کاربر خالی می باشد");
		}

		public override void Validate()
		{
			try
			{
				_ = CheckSignExisis();
			}
			catch (Exception ex)
			{
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, ex.Message));
			}
		}

		// <inheritdoc/>
		public override async Task HandledExceptions(HttpStResult? result, Exception? exception)
		{
			EntityException log;
			// اگر درخواست به خطا خورد
			if (RequestIdExists())
				log = new StartException() { RequestId = RequestId };
			else // اصلا افتتاح حساب نشده و خطا بوجود آمده
				log = new EntityException();
			
			log.SetException(exception);
			log.SetMessageAndStatusCode(result);
			
			if (RequestIdExists())
				await StartExceptionBl.Post((StartException)log);
			else
				await EntityExceptionBl.Post(log);
		}
	}
}