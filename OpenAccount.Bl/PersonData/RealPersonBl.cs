using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.Entities.PersonData;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Bl.PersonData
{
	internal sealed class RealPersonBl : BasePersonBl<RealPerson, IRealPersonIdentificationRepository>, IRealPersonBl
	{
		public RealPersonBl(IRealPersonIdentificationRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}

		/// <summary>
		/// کاربر لاگین کرده زنده است؟
		/// </summary>
		/// <returns></returns>
		public Task<bool> IsUserAlive(Guid requestid) => LogicRepository.IsUserAlive(requestid);
	}
}