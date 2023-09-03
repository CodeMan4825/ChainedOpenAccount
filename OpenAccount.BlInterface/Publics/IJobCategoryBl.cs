using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Publics;

namespace OpenAccount.BlInterface.Publics
{
	/// <summary>
	/// گروه مشاغل
	/// </summary>
	public interface IJobCategoryBl : IBaseLogic<JobCategory, byte>
	{
		/// <summary>
		/// مشاغل فعال را برگردان
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<JobCategory>> GetActives();
	}
}