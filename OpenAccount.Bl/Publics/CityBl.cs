using Microsoft.AspNetCore.Http;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.BlInterface.Publics;
using OpenAccount.Entities.Publics;
using OpenAccount.RepositoryInterface.Publics;

namespace OpenAccount.Bl.Publics
{
	/// <summary>
	/// شهر
	/// </summary>
	internal sealed class CityBl : BaseRoLogic<City, ICityRepository, int>, ICityBl
	{
		public CityBl(ICityRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}

		/// <summary>
		/// شهرهای یک استان
		/// </summary>
		/// <param name="provinceId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<IEnumerable<City>> GetByProvinceId(int provinceId) => await LogicRepository.GetByProvinceId(provinceId);
	}
}