using Microsoft.AspNetCore.Http;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.BlInterface.Publics;
using OpenAccount.Entities.Publics;
using OpenAccount.RepositoryInterface.Publics;

namespace OpenAccount.Bl.Publics
{
	/// <summary>
	/// استان
	/// </summary>
	internal sealed class ProvinceBl : BaseRoLogic<Province, IProvinceRepository, int>, IProvinceBl
	{
		public ProvinceBl(IProvinceRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}
	}
}