using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.BlInterface.Accounts;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.BtmsDtos;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Bl.Requests
{
	internal sealed class RequestBl : BaseLogic<Request, IRequestRepository, Guid>, IRequestBl
	{
		public RequestBl(IRequestRepository logicRepository,
			IAccountTypeSettingBl accountTypeSetting,
			IOptions<BtmsSettingDto> options,
			IHttpContextAccessor accessor) :
			base(logicRepository, accessor)
		{
			AccountTypeSetting = accountTypeSetting;
			BtmsSetting = options.Value;
		}

		private readonly IAccountTypeSettingBl AccountTypeSetting;
		private readonly BtmsSettingDto BtmsSetting;

		/// <summary>
		/// Get requests by userId.
		/// </summary>
		/// <returns></returns>
		public override Task<IEnumerable<Request>> Get() => Task.FromResult(LogicRepository.AsQuery().Where(x => x.PersonId == UserData.UserId).ToList().AsEnumerable());

		/// <summary>
		/// تنظیمات فعال درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId">شناسه درخواست</param>
		/// <returns></returns>
		public async Task<AccountTypeSetting> GetAccountTypeSetting(Guid requestId) => await LogicRepository.GetAccountTypeSetting(requestId);

		/// <summary>
		/// لیست حساب هایی که برای افتتاح حساب در دسترسند
		/// </summary>
		/// <returns>لیست حساب ها</returns>
		/// <exception cref="StException.DataNotFound(string)">لیست نوع حساب های موجود در سامانه</exception>
		/// <exception cref="StException.ServiceUnavailable(string)">دریافت اطلاعات حساب از بانک</exception>
		public async Task<IEnumerable<AvalableRequestsDto>> GetAvalableRequests()
		{
			var result = new List<Request>();
			// لیست نوع حساب های موجود در باجت
			var accountTypes = await AccountTypeSetting.Get();
			if (accountTypes == null || !accountTypes.Any())
				throw StException.DataNotFound("لیست نوع حساب های موجود در سامانه موجود نمی باشد");

			// اطلاعات حساب های مشتری در بانک
			var client = HttpClients.CreateClientWithCustomHeaders(GetUserDataFromHeaderAsDictionary());
			var btms = await HttpClients.Get<HttpSimorghApiResponseDto<IEnumerable<NacAllDataByNationalCodeDto>>>(
				client,
				BtmsSetting.MainUrl,
				string.Format(BtmsSetting.NacAllDataByNationalCode, UserData.NationalCode));

			// سرویس پاسخ نداد
			if (btms == null || !btms.ActionCodeOk)
				throw StException.ServiceUnavailable("عدم دریافت پاسخ از سرویس دریافت اطلاعات حساب کاربر از بانک");

			foreach (var accountType in accountTypes)
			{	// حساب کاربر با نرع خاص
				var req = await GetUserRequestByAccountType(accountType.AccountType);
				if (req == null) // کاربر حسابی با این نوع ندارد
				{
					var newReq = new Request { AccountType = accountType.AccountType, RequestStateType = RequestStateType.None };

					if (btms.Data != null)
						foreach (var item in btms.Data)
							if (int.Parse(item.Account.accGrp) == int.Parse(accountType.AccountGroupId) && int.Parse(item.Account.branchCode) == 3310)
							{
								newReq.UserAccount = new UserAccount
								{
									AccountNumber = item.Account.accNo,
									ShebaNumber = OpenAccountUtility.CalcShebaNumber(item.Account.accNo),
								};
								newReq.RequestStateType = RequestStateType.Finished;
								break;
							}
					result.Add(newReq);
				}
				else// حساب دارد
				{   // اگر افتتاح حساب کامل شده بود
					if (req.RequestStateType > RequestStateType.UserAccount)
						req.UserAccount = await LogicRepository.GetUserAccount(req.Id);
					result.Add(req);
				}
			}
			return result.Select(x => new AvalableRequestsDto
			{
				Id = x.Id,
				AccountType = x.AccountType,
				AccountTypeDescription = Utility.GetEnumDescription(x.AccountType),
				RequestStateType = x.RequestStateType,
				RequestStateTypeDescription = x.RequestStateTypeDescription,
				ShebaNumber = x.UserAccount == null ? string.Empty : x.UserAccount.ShebaNumber,
				AccountNumber = x.UserAccount == null ? string.Empty : x.UserAccount.AccountNumber
			});
		}

		/// <summary>
		/// Get request of user by account type.
		/// </summary>
		/// <param name="accountType"></param>
		/// <returns>Request</returns>
		public Task<Request?> GetUserRequestByAccountType(AccountType accountType) => Task.FromResult(LogicRepository.AsQuery().Where(x => x.PersonId == UserData.UserId && x.AccountType == accountType).FirstOrDefault());

		/// <summary>
		/// Get request with RequestLog and PersonInfo data.
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>Request</returns>
		public async Task<Request> GetWithLogPersonInfo(Guid requestId) => await LogicRepository.GetWithLogPersonInfo(requestId);

		/// <summary>
		/// شماره حساب را بده
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public async Task<string> GetAccountNumber(Guid requestId) => await LogicRepository.GetAccountNumber(requestId);
	}
}