using OpenAccount.Entities.Publics;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Publics;

namespace OpenAccount.Repository.Publics
{
	/// <summary>
	/// تحصیلات
	/// </summary>
	internal sealed class EducationRepository : ApplicationRoRepository<Education, byte>, IEducationRepository
	{
		public EducationRepository(AppDbContext context) : base(context)
		{
		}
	}
}