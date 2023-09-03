using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.BlInterface.Publics.Wallets;
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
	/// اطلاعات پستی
	/// </summary>
	internal sealed class RealPersonPostInqueryBl : OpenAccountChainedRoBl<RealPerson, IRealPersonPostInqueryRepository, Guid>, IRealPersonPostInqueryBl
	{
		public RealPersonPostInqueryBl(
			IRealPersonPostInqueryRepository logicRepository,
			IRealPersonInfoCompletionBl preRequest,
			IHttpContextAccessor accessor,
			IRequestBl requestBl,
			IWalletStatusBl walletStatus,
			IWithdrawalFromWalletBl withdrawalFromWallet,
			IOptions<BtmsSettingDto> btms,
			IPersonExceptionBl exceptionBl,
			IRequestStateLogBl requestLog) : base(logicRepository, preRequest, RequestStateType.PersonPostInquery, accessor, requestLog)
		{
			RequestBl = requestBl;
			WalletStatus = walletStatus;
			WithdrawalFromWallet = withdrawalFromWallet;
			Btms = btms.Value;
			ExceptionBl = exceptionBl;
		}

		private readonly IRequestBl RequestBl;
		private readonly BtmsSettingDto Btms;
		private readonly IWalletStatusBl WalletStatus;
		private readonly IWithdrawalFromWalletBl WithdrawalFromWallet;
		private readonly IPersonExceptionBl ExceptionBl;

		public override RequestStateType GetNextStep() => RequestStateType.CardOrder;

		public override void Validate()
		{   // Get RealPerson with active RealPersonInfo and active Address.
			var realPerson = LogicRepository.GetRealPersonWithInfoAddress(UserData.UserId).Result;
			if (realPerson == null || realPerson.RealPersonInfos == null || !realPerson.RealPersonInfos.Any() || realPerson.Addresses == null || !realPerson.Addresses.Any())
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "اطلاعات محل سکونت کامل نمی باشد"));
			if (string.IsNullOrEmpty(realPerson.Addresses.First().PostalCode))
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "اطلاعات کد پستی موجود نمی باشد"));
		}

		/// <summary>
		/// استعلام کدپستی
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		public async Task<AddressToConfirmPostCodeDto> PostInquiry(string postalCode)
		{   //درخواست را بده
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();

			// Get RealPerson with active RealPersonInfo and active Address.
			var realPerson = await LogicRepository.GetRealPersonWithInfoAddress(UserData.UserId);
			if (realPerson == null || realPerson.RealPersonInfos == null || !realPerson.RealPersonInfos.Any())
				throw StException.DataNotFound("اطلاعات پرسنلی موجود نمی باشد");

			// مبلغ مورد نیاز برای استعلام کد پستی را برگردان
			var postInquiryPrice = (await RequestBl.GetAccountTypeSetting(RequestId)).PostalCodeInquiry;
			if (postInquiryPrice > 0)
				// آیا برداشت از کیف پول با نوع خاص انجام شده است؟
				if (!await WithdrawalFromWallet.ControlWithdrawal(RequestId, false, Entities.Publics.Wallets.EventType.PostalCodeInquiry))
				{   // برداشت انجام نشده است، آخرین وضعیت کیف پول
					var wallet = await WalletStatus.GetWalletStatusForType(Entities.Publics.Wallets.EventType.PostalCodeInquiry);
					if (wallet.NeededCharge == 0) // موجودی کافیست، پس برداشت کن
						await WithdrawalFromWallet.Withdrawal(postInquiryPrice, Entities.Publics.Wallets.EventType.PostalCodeInquiry, RequestId);
					else
						throw StException.ResultNotAcceptable("موجودی کافی نیست");
				}

			// استعلام کدپستی
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var data = await HttpClients.Post<HttpSimorghApiResponseDto<AddressByPostCodeResponseDto>>(
				client,
				Btms.MainUrl,
				Btms.PostalCodeInquiry,
				new
				{
					ClientBatchID = "0",
					postCodes = new List<object>()
					{
						new { ClientRowID = 0, PostCode = postalCode }
					},
					Signature = "0"
				}) ?? throw StException.ServiceUnavailable("عدم دریافت پاسخ از سرویس استعلام کدپستی");

			if (data.Data != null && data.Data.Data != null && data.Data.Data.Any() && data.ActionCodeOk)// برداشت انجام شده از کیف پول مورد استفاده قرار گرفت
				await WithdrawalFromWallet.MakeUsedWithdrawal(RequestId, Entities.Publics.Wallets.EventType.PostalCodeInquiry);

			Address addressEntity;
			if (realPerson.Addresses != null)
			{
				if (realPerson.Addresses.Any())
					addressEntity = realPerson.Addresses.Where(x => x.IsActive).First().Clone();
				else
					addressEntity = new Address();
				realPerson.Addresses.ToList().ForEach(x => { x.IsActive = false; });
			}
			else
			{
				addressEntity = new Address();
				realPerson.Addresses = new List<Address>();
			}
			var info = realPerson.RealPersonInfos.First();
			info.ErrorCode = data.ActionCode;
			info.ErrorMessage = data.ActionMessage;
			if (data.Data != null && data.Data.Data != null && data.Data.Data.Any())
			{
				var postCode = data.Data.Data.First();
				var address = postCode.Result;
				addressEntity.Id = Guid.NewGuid();
				addressEntity.Person = realPerson;
				addressEntity.PostalCode = postCode.Postcode;
				addressEntity.BuildingName = address.BuildingName;
				addressEntity.Street = address.Street;
				addressEntity.Description = address.Description;
				addressEntity.Floor = address.Floor;
				addressEntity.FullAddress = address.FullAddress;
				addressEntity.HouseNumber = address.HouseNumber;
				addressEntity.LocalityCode = address.LocalityCode;
				addressEntity.LocalityType = address.LocalityType;
				addressEntity.Province = address.Province;
				addressEntity.SideFloor = address.SideFloor;
				addressEntity.SubLocality = address.SubLocality;
				addressEntity.Street2 = address.Street2;
				addressEntity.TownShip = address.TownShip;
				addressEntity.Village = address.Village;
				addressEntity.Zone = address.Zone;
				addressEntity.IsActive = true;
				addressEntity.SysDate = DateTime.Now;
				realPerson.Addresses.Add(addressEntity);
			}
			if (request.RequestStateType <= RequestStateType.PersonPostInquery && data.ActionCodeOk)
			{
				_ = await GoToNextStep(request);        // برو به مرحله ی بعد
				await RequestBl.Put(request, false);    // مرحله ی درخواست را بروز کن
			}
			await LogicRepository.Update(realPerson);

			if (!data.ActionCodeOk)
				throw StException.ResultNotAcceptable($"اطلاعات هویتی : {data.ActionMessage}");
			if (data.Data == null)
				StException.ServiceUnavailable("عدم دریافت پاسخ از سرویس استعلام کدپستی");
			return new AddressToConfirmPostCodeDto
			{
				FullAddress = addressEntity.GetFullAddress,
				PostalCode = addressEntity.PostalCode
			};
		}

		/// <summary>
		/// استعلام کدپستی بصورت آفلاین
		/// </summary>
		/// <param name="postalCode">کد پستی</param>
		/// <returns></returns>
		public async Task<AddressToConfirmPostCodeDto> PostalCodeOfflineInquiry()
		{   //درخواست را بده
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();

			// Get RealPerson with active RealPersonInfo and active Address.
			var realPerson = await LogicRepository.GetRealPersonWithInfoAddress(UserData.UserId);
			if (realPerson == null || realPerson.RealPersonInfos == null || !realPerson.RealPersonInfos.Any())
				throw StException.DataNotFound("اطلاعات پرسنلی موجود نمی باشد");

			Address addressEntity;
			if (realPerson.Addresses != null)
			{
				if (realPerson.Addresses.Any())
					addressEntity = realPerson.Addresses.Where(x => x.IsActive).First().Clone();
				else
					addressEntity = new Address();
				realPerson.Addresses.ToList().ForEach(x => { x.IsActive = false; });
			}
			else
			{
				addressEntity = new Address();
				realPerson.Addresses = new List<Address>();
			}
			var postalCode = string.IsNullOrEmpty(addressEntity.PostalCode) ? realPerson.RealPersonInfos.First().PostalCode : addressEntity.PostalCode;

			// استعلام کدپستی
			var data = await HttpClients.Get<Address>(
				HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary()),
				Btms.MainUrl,
				string.Format(Btms.PostalCodeOfflineInquiry, postalCode)) ?? throw StException.ServiceUnavailable("عدم دریافت پاسخ از سرویس استعلام کد پستی");

			addressEntity.Id = Guid.NewGuid();
			addressEntity.Person = realPerson;
			addressEntity.PostalCode = postalCode;
			addressEntity.BuildingName = data.BuildingName;
			addressEntity.Street = data.Street;
			addressEntity.Description = data.Description;
			addressEntity.Floor = data.Floor;
			addressEntity.FullAddress = data.FullAddress;
			addressEntity.HouseNumber = data.HouseNumber;
			addressEntity.LocalityCode = data.LocalityCode;
			addressEntity.LocalityType = data.LocalityType;
			addressEntity.Province = data.Province;
			addressEntity.SideFloor = data.SideFloor;
			addressEntity.SubLocality = data.SubLocality;
			addressEntity.Street2 = data.Street2;
			addressEntity.TownShip = data.TownShip;
			addressEntity.Village = data.Village;
			addressEntity.Zone = data.Zone;
			addressEntity.IsActive = true;
			addressEntity.SysDate = DateTime.Now;
			realPerson.Addresses.Add(addressEntity);

			_ = await GoToNextStep(request);        // برو به مرحله ی بعد
			await RequestBl.Put(request, false);    // مرحله ی درخواست را بروز کن
			await LogicRepository.Update(realPerson);

			return new AddressToConfirmPostCodeDto
			{
				FullAddress = addressEntity.GetFullAddress,
				PostalCode = postalCode
			};
		}

		public override async Task HandledExceptions(HttpStResult? result, Exception? exception)
		{
			var entity = new PersonException(RequestStateType.PersonPostInquery) { RequestId = RequestId };
			entity.SetException(exception);
			entity.SetMessageAndStatusCode(result);
			await ExceptionBl.Post(entity);
		}
	}
}