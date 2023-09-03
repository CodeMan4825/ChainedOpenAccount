using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.BlInterface.Publics.Wallets;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.BtmsDtos;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Accounts;
using System.Text.RegularExpressions;

namespace OpenAccount.Bl.Accounts
{
	/// <summary>
	/// افتتاح حساب کاربر
	/// </summary>
	internal sealed class UserAccountBl : OpenAccountChainedBl<UserAccount, IUserAccountRepository, Guid>, IUserAccountBl
	{
		public UserAccountBl(IUserAccountRepository logicRepository,
			IRequestDigitalSignatureBl preRequest,
			IHttpContextAccessor accessor,
			IRequestBl requestBl,
			IWalletStatusBl walletStatus,
			IWithdrawalFromWalletBl withdrawalFromWallet,
			IOptions<BtmsSettingDto> btmsSetting,
			IRequestStateLogBl requestLog) :
			base(logicRepository, preRequest, RequestStateType.UserAccount, accessor, requestLog)
		{
			RequestBl = requestBl;
			WalletStatus = walletStatus;
			WithdrawalFromWallet = withdrawalFromWallet;
			BtmsSetting = btmsSetting.Value;
		}

		private readonly IRequestBl RequestBl;
		private readonly IWalletStatusBl WalletStatus;
		private readonly IWithdrawalFromWalletBl WithdrawalFromWallet;
		private readonly BtmsSettingDto BtmsSetting;

		public override RequestStateType GetNextStep() => RequestStateType.RealSignatureToBank;

		/// <summary>
		/// افتتاح حساب
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>شماره حساب</returns>
		public async Task<string> OpenAccount(Guid requestId)
		{
			var userAcc = await Get(requestId);
			if (userAcc != null && !string.IsNullOrEmpty(userAcc.AccountNumber))
				throw StException.DataDublicate("برای درخواست شما افتتاح حساب انجام شده است");

			var setting = await RequestBl.GetAccountTypeSetting(requestId) ?? throw StException.DataNotFound("مشکل در تنظیمات حساب کاربر");
			var request = await RequestBl.GetWithLogPersonInfo(requestId) ?? throw StException.DataNotFound("مشکل در دریافت اطلاعات کاربر");
			if (request.Person == null || request.Person is not RealPerson)
				throw StException.DataNotFound("مشکل در دریافت اطلاعات کاربر");
			if (request.Person.City == null)
				throw StException.DataNotFound("مشکل در دریافت اطلاعات شهر محل تولد");
			if (request.Person.Addresses == null || !request.Person.Addresses.Any())
				throw StException.DataNotFound("مشکل در دریافت اطلاعات آدرس محل سکونت");
			if (((RealPerson)request.Person).RealPersonInfos == null || !((RealPerson)request.Person).RealPersonInfos.Any())
				throw StException.DataNotFound("مشکل در دریافت اطلاعات تکمیلی کاربر");

			var realPersonInfo = ((RealPerson)request.Person).RealPersonInfos.First() ?? throw StException.DataNotFound("مشکل در دریافت اطلاعات تکمیلی کاربر");
			if (string.IsNullOrEmpty(request.Person.Addresses.First().PostalCode))
				throw StException.DataNotFound("مشکل در دریافت اطلاعات پستی کاربر");

			// اطلاعات حساب های مشتری در بانک
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var btms = await HttpClients.Get<HttpSimorghApiResponseDto<IEnumerable<NacAllDataByNationalCodeDto>>>(
				client,
				BtmsSetting.MainUrl,
				string.Format(BtmsSetting.NacAllDataByNationalCode, UserData.NationalCode)) ?? throw StException.ServiceUnavailable("عدم دریافت اطلاعات حساب از بانک");

			if (!btms.ActionCodeOk) throw StException.ServiceUnavailable(btms.ActionMessage);
			if (btms.Data == null) throw StException.ServiceUnavailable("عدم دریافت اطلاعات حساب از بانک");

			foreach (var item in btms.Data)
				if (int.Parse(item.Account.accGrp) == int.Parse(setting.AccountGroupId) && int.Parse(item.Account.branchCode) == 3310)
					throw StException.DataDublicate("این نوع حساب از پیش در بانک افتتاح شده است");

			// آخرین وضعیت کیف پول مشتری لاگین کرده را برای کمینه ی افتتاح حساب برمی گرداند
			var wallet = await WalletStatus.GetWalletStatusForType(Entities.Publics.Wallets.EventType.OpenAccount);
			if (wallet.NeededCharge > 0)
				throw StException.ResultNotAcceptable(new ValidateBalanceExceptionDto(LogicType, "کمبود موجودی", wallet.NeededCharge));

			/*// آیا برداشت از کیف پول با نوع خاص انجام شده است؟
			if (!await WithdrawalFromWallet.ControlWithdrawal(requestId, false, Entities.Publics.Wallets.EventType.OpenAccount))
				// از کیف پول برداشت کن
				await WithdrawalFromWallet.Withdrawal(wallet.NeededBalance, Entities.Publics.Wallets.EventType.OpenAccount, requestId);*/

			var openAccountData = new BtmsOpenAccountRequest
			{
				Account = new BtmsAccount
				{
					AccountGroup = setting.AccountGroupId.PadLeft(3, '0'),
					AccountNumber = "",
					AccountTypeCode = 1, // نوع حساب : کد 1 شخصی،کد 2 حقوقی،کد 3 مشترك
					Ast = 0,
					BillControl = 1, // کد کنترلی حساب ای تی ام تعداد نسخ صورتحساب: ایجاد، تغییر، ابطال، صدور مجدد
					BillType = 3, // نوع صورتحساب 1-روزانه 2- هفتگی 3- ماهانه 4- عدم ارسال
					BranchCode = "003310",
					Cbi = null, // براي حساب هاي غیر شخصی
					Cbk = null, // براي حساب هاي غیر شخصی
					Centruy = 14, // مقدار 13
					Combination = 0, // در شعبه به کار نمی رود ، 0
					CounterCode = "003310", // کد باجه
					CurrencyCode = 0, // کد ارز
					ExporterBranchCode = "00000", // کد شعبه صادر کننده
					Faragir = "0",
					FaragirAccountNumber = "", // شماره حساب جاري فراگیر براي اتصال به حساب کارت
					FormNumber = "0", // شماره فرم
					IsicCode = "0",
					LoanUse = "00", // فقط براي حسابهاي تسهیلاتی حوزه ائی که تسهیلات به آن تعلق میگیرد
					OperatorNumber = 1, // شماره اپراتو ر
					ReagentAccount = 0,
					SendToNasim = true,
					ShareCode = 0
				},
				AccountNasimInfo = new BtmsAccountNasimInfo
				{
					AccountOpenerName = $"{request.Person.Name} {((RealPerson)request.Person).Family}",
					WithdrawalType = "NORMAL"
				},
				LegalPerson = null,
				RealPerson = new BtmsRealPerson
				{
					Address = request.Person.Addresses.First().FullAddress.Substring(0, 30),
					BirthDate = CastUtils.DateTimeToFarsiStr(request.Person.Date),
					City = request.Person.City.Name,
					EducationCode = CastUtils.StrToInt(realPersonInfo.EducationId.ToString()), //3
					Email = "nothing@stts.com",
					FatherName = ((RealPerson)request.Person).FatherName,
					Fax = request.Person.Addresses.First().MobileNumber,
					FirstName = request.Person.Name,
					InComeCode = 0,
					IsForeign = 0, // در صورتی که فرد خارجی باشد برابر با یک
					JobCode = realPersonInfo.JobId == null ? 1 : realPersonInfo.JobId.Value, //32
					JobTypeCode = CastUtils.StrToInt(realPersonInfo?.Job.JobCategoryId.ToString()), //6
					LastName = ((RealPerson)request.Person).Family,
					ManageExpirationDate = "1300/00/00", // تاریخ انقضاء اختیارات مدیر
					NationalCode = request.Person.NationalCode,
					OperatorNumber = 1,
					PhoneNumber = request.Person.Addresses.First().MobileNumber,
					PostalCode = request.Person.Addresses.First().PostalCode,
					RegisterLocationCode = 4,
					RegisterLocationOfficeCode = 1, // حوزه محل صدور شناسنامه
					RegisterValidity = "1", // اعتبار استعلام از ثبت احوا ل
											//کد ارتباط: کد 1مدیر عامل،کد 2 سهامدار،کد 3 عضو هیئت
											//مدیره،کد 4 رئیس هیئت مدیره،کد 5 نایب رئیس هیئت
											//مدیره،کد 6 مدیر سابق،کد 7 امضادار سابق،کد 8 مدیر عامل
											//و عضو هیئت مدیره،کد 9 سایر سمت هاي داراي حق امضاء
					RelationCode = 9,
					RowNumber = 0,
					SexTypeCode = ((RealPerson)request.Person).IsMale ? 1 : 2, // کد جنسیت: کد 1 آقا،کد 2 خانم،کد 3 مشترك
					SharePercentCode = 0, // کد درصد سهم
					SharesPercent = 0, // درصد سهم
					SignStatusCode = 1, // کد نوع امضا 1- عادي 2 – صاحب امضا
					SocialIdentityLetterSeries = realPersonInfo.SocialIdentityExtensionSeries[0].ToString(),
					SocialIdentityNumber = CastUtils.StrToInt(realPersonInfo.SocialIdentityNumber.ToString()), // شماره شناسنامه
					SocialIdentityNumericSeries = CastUtils.StrToInt(Regex.Match(realPersonInfo.SocialIdentityExtensionSeries, @"\d+").Value),
					SocialIdentitySeries = realPersonInfo.SocialIdentitySeries,
					TerminalNumber = "03",
					UpdateDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), // تاریخ به روز رسانی
																			   //EnglishName = request.Person.LatinName,
																			   //EnglishFamily = ((RealPerson)request.Person).LatinFamily,
																			   //Province = string.Empty,
																			   //Language = "2",
					Mobile = request.Person.Addresses.First().MobileNumber
				},
				Signatory = new BtmsSignatory { SignatoryList = null }
			};
			var req = await RequestBl.Get(requestId) ?? throw StException.RequestIdNotFound();
			client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var btmsOpenAccount = await HttpClients.Post<HttpSimorghApiResponseDto<OpenAccountResponseDto>>(client, BtmsSetting.MainUrl, BtmsSetting.OpenAccount, openAccountData);
			var account = userAcc ?? new UserAccount { SysDate = DateTime.Now };
			account.Request = req;			

			if (btmsOpenAccount.ActionCodeOk && btmsOpenAccount.Data != null && btmsOpenAccount.Data.Response != null && !string.IsNullOrEmpty(btmsOpenAccount.Data.Response.AccountNumber))
			{
				if (wallet.NeededBalance > 0) // برداشت انجام شده از کیف پول مورد استفاده قرار گرفت
					await WithdrawalFromWallet.MakeUsedWithdrawal(requestId, Entities.Publics.Wallets.EventType.OpenAccount, false);
				account.AccountNumber = btmsOpenAccount.Data.Response.AccountNumber;
				_ = await GoToNextStep(req);//برو به مرحله ی بعد
			}
			account.UserAccountLogs = new List<UserAccountLog>
			{
				new UserAccountLog
				{
					ActionMessage = btmsOpenAccount.ActionMessage,
					ActionCode = btmsOpenAccount.ActionCode,
					ErrorMessages = btmsOpenAccount.ErrorMessages == null ? string.Empty : btmsOpenAccount.ErrorMessages.ToString(),
					ReferenceNumber = btmsOpenAccount.ReferenceNumber,
					TraceNumber = btmsOpenAccount.TraceNumber,
					SysDate = DateTime.Now,
					ResponseCode = btmsOpenAccount.Data?.Message?.Code ?? 0,
					ResponseText = btmsOpenAccount.Data?.Message?.Text ?? string.Empty,
					UserAccount = account
				}
			};
			await (userAcc == null ? Post(account) : Put(account));
			if (string.IsNullOrEmpty(account.AccountNumber))
				throw StException.ServiceUnavailable(string.IsNullOrEmpty(btmsOpenAccount.ActionMessage) ? "خطای ناشناخته در افتتاح حساب" : btmsOpenAccount.ActionMessage);
			
			return account.AccountNumber;
		}

		public override void Validate()
		{
			var data = LogicRepository.AsQuery().Where(x => x.Id == RequestId && !string.IsNullOrEmpty(x.AccountNumber));
			if (data == null || !data.Any())
				StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "افتتاح حساب کاربر انجام نشده است"));
		}
	}
}