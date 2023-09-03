using Microsoft.AspNetCore.Http;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.BlInterface.Publics;
using OpenAccount.Entities.Publics;
using OpenAccount.RepositoryInterface.Publics;

namespace OpenAccount.Bl.Publics
{
	/// <summary>
	/// تحصیلات
	/// </summary>
	internal sealed class EducationBl : BaseRoLogic<Education, IEducationRepository, byte>, IEducationBl
	{
		public EducationBl(IEducationRepository logicRepository, IHttpContextAccessor accessor, bool userDataNeeded = true) : base(logicRepository, accessor, userDataNeeded)
		{
		}
	}
}