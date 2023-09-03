using OpenAccount.Entities.Publics;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Publics;

namespace OpenAccount.Repository.Publics
{
	/// <summary>
	/// استان
	/// </summary>
	internal sealed class ProvinceRepository : ApplicationRoRepository<Province, int>, IProvinceRepository
	{
		public ProvinceRepository(AppDbContext context) : base(context)
		{
		}
	}
}