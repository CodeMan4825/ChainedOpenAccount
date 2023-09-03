using OpenAccount.Entities.Publics;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Publics
{
	/// <summary>
	/// گروه مشاغل
	/// </summary>
	public interface IJobCategoryRepository : IBaseRepository<JobCategory, byte>
	{
		/// <summary>
		/// مشاغل فعال را برگردان
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<JobCategory>> GetActives();
	}
}