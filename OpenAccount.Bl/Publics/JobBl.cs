using Microsoft.AspNetCore.Http;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.BlInterface.Publics;
using OpenAccount.Entities.Publics;
using OpenAccount.RepositoryInterface.Publics;

namespace OpenAccount.Bl.Publics
{
	/// <summary>
	/// مشاغل
	/// </summary>
	internal sealed class JobBl : BaseLogic<Job, IJobRepository, int>, IJobBl
	{
		public JobBl(IJobRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}

		/// <summary>
		/// لیست مشاغل فعال یک گروه شغلی
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Job>> GetJobsByCategory(byte categoryId) => await LogicRepository.GetJobsByCategory(categoryId);
	}
}