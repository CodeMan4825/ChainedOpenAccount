using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Publics;

namespace OpenAccount.BlInterface.Publics
{
	/// <summary>
	/// شهر
	/// </summary>
	public interface ICityBl : IBaseRoLogic<City, int>
	{
		/// <summary>
		/// شهرهای یک استان
		/// </summary>
		/// <param name="provinceId"></param>
		/// <returns></returns>
		Task<IEnumerable<City>> GetByProvinceId(int provinceId);
	}
}