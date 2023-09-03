using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Publics;

namespace OpenAccount.BlInterface.Publics
{
	/// <summary>
	/// مشاغل
	/// </summary>
	public interface IJobBl : IBaseLogic<Job, int>
	{
		/// <summary>
		/// لیست مشاغل فعال یک گروه شغلی
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		Task<IEnumerable<Job>> GetJobsByCategory(byte categoryId);
	}
}