using Microsoft.AspNetCore.Mvc;
using OpenAccount.Api.Infrastructure;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Requests;

namespace OpenAccount.Api.Controllers.Requests
{
	/// <summary>
	/// درخواست اولیه ی افتتاح حساب
	/// </summary>
	public sealed class RequestStartController : OpenAccountChainedController<Request, IRequestStartBl, Guid>
	{
		public RequestStartController(IConfiguration configuration, 
			IHttpContextAccessor accessor,
			IRequestStartBl baseLogic) :
			base(configuration, accessor, baseLogic, RequestStateType.Start, false)
		{
		}

		/// <summary>
		/// درج درخواست با نوع خاص
		/// </summary>
		/// <param name="accountType"></param>
		/// <returns></returns>
		[HttpPost("PostForRealPerson")]
		public async Task PostForRealPerson(AccountType accountType) => await ControllerLogic.PostForRealPesron(accountType);		
	}
}