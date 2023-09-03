using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Publics;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Publics;

namespace OpenAccount.Repository.Publics
{
	/// <summary>
	/// شهر
	/// </summary>
	internal sealed class CityRepository : ApplicationRoRepository<City, int>, ICityRepository
	{
		public CityRepository(AppDbContext context) : base(context)
		{
		}

		/// <summary>
		/// شهرهای یک استان
		/// </summary>
		/// <param name="provinceId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<IEnumerable<City>> GetByProvinceId(int provinceId) => await Entities.Where(x => x.ProvinceId == provinceId).ToListAsync();
	}
}