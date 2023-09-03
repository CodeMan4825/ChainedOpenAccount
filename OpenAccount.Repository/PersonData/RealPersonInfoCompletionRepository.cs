using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.PersonData;
using OpenAccount.Publics;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Repository.PersonData
{
	/// <summary>
	/// اطلاعات تکمیلی
	/// </summary>
	internal sealed class RealPersonInfoCompletionRepository : BasePersonRepository<RealPerson>, IRealPersonInfoCompletionRepository
	{
		public RealPersonInfoCompletionRepository(AppDbContext context) : base(context)
		{
		}

		/// <summary>
		/// Get RealPerson with active RealPersonInfo.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<RealPerson?> GetRealPersonWithInfo(Guid id)
		{
			var info = await Context.RealPersonInfos.Where(x => x.RealPersonid == id && x.IsActive).AsNoTracking().FirstOrDefaultAsync();
			var result = await Entities.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
			if (info != null && result != null)
				result.RealPersonInfos = new List<RealPersonInfo> { info };
			else
				return null;

			return result;
		}

		public override async Task Update(RealPerson entity, bool save = true)
		{
			if (entity.RealPersonInfos == null || !entity.RealPersonInfos.Any())
				throw StException.DataNotFound("اطلاعات تکمیلی شخص");

			var info = entity.RealPersonInfos.First();
			Context.Attach(info).State = EntityState.Modified;

			await base.Update(entity, save);
		}

		protected override void SetFieldsForUpdate(RealPerson foundObj, RealPerson data) => foundObj.CityId = data.CityId;
	}
}