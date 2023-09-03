using OpenAccount.Entities.Publics;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Publics
{
	/// <summary>
	/// مشاغل
	/// </summary>
	public interface IJobRepository : IBaseRepository<Job, int>
	{
		/// <summary>
		/// لیست مشاغل فعال یک گروه شغلی
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		Task<IEnumerable<Job>> GetJobsByCategory(byte categoryId);
	}
}