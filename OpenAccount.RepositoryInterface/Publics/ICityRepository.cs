using OpenAccount.Entities.Publics;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Publics
{
	/// <summary>
	/// شهر
	/// </summary>
	public interface ICityRepository : IBaseRoRepository<City, int>
	{
		/// <summary>
		/// شهرهای یک استان
		/// </summary>
		/// <param name="provinceId"></param>
		/// <returns></returns>
		Task<IEnumerable<City>> GetByProvinceId(int provinceId);
	}
}