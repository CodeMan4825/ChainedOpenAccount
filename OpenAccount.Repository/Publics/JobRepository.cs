using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Publics;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Publics;

namespace OpenAccount.Repository.Publics
{
	/// <summary>
	/// مشاغل
	/// </summary>
	internal sealed class JobRepository : ApplicationRepository<Job, int>, IJobRepository
	{
		public JobRepository(AppDbContext context) : base(context)
		{
		}

		/// <summary>
		/// لیست مشاغل فعال یک گروه شغلی
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Job>> GetJobsByCategory(byte categoryId) => await Entities.Where(x => x.JobCategoryId == categoryId && x.IsActive).ToListAsync();
	}
}