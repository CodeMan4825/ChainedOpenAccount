using OpenAccount.BlInterface.PersonData;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Requests;

namespace OpenAccount.Api.Controllers.PersonData
{
	public abstract class BasePersonController<TPerson, TLogic> : OpenAccountChainedRoController<TPerson, TLogic, Guid>
		where TPerson : Person
		where TLogic : IPersonChainedBl<TPerson>
	{
		protected BasePersonController(IConfiguration configuration, IHttpContextAccessor accessor, TLogic baseLogic, RequestStateType controllerEnum) : base(configuration, accessor, baseLogic, controllerEnum)
		{
		}
	}
}