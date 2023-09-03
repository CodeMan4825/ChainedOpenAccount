using Microsoft.AspNetCore.Http;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.BlInterface.Publics;
using OpenAccount.Entities.Publics;
using OpenAccount.RepositoryInterface.Publics;

namespace OpenAccount.Bl.Publics
{
	/// <summary>
	/// گروه مشاغل
	/// </summary>
	internal sealed class JobCategoryBl : BaseLogic<JobCategory, IJobCategoryRepository, byte>, IJobCategoryBl
	{
		public JobCategoryBl(IJobCategoryRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}

		/// <summary>
		/// مشاغل فعال را برگردان
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<JobCategory>> GetActives() => await LogicRepository.GetActives();
	}
}