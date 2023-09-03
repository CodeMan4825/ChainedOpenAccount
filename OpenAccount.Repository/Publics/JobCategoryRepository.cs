using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Publics;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Publics;

namespace OpenAccount.Repository.Publics
{
	/// <summary>
	/// گروه مشاغل
	/// </summary>
	internal sealed class JobCategoryRepository : ApplicationRepository<JobCategory, byte>, IJobCategoryRepository
	{
		public JobCategoryRepository(AppDbContext context) : base(context)
		{
		}

		/// <summary>
		/// مشاغل فعال را برگردان
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<JobCategory>> GetActives() => await Entities.Where(x => x.IsActive).ToListAsync();
	}
}