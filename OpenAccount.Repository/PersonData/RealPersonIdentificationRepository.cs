using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.PersonData;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Repository.PersonData
{
	/// <summary>
	/// اشخاص حقیقی
	/// </summary>
	internal sealed class RealPersonIdentificationRepository : BasePersonRepository<RealPerson>, IRealPersonIdentificationRepository
	{
		public RealPersonIdentificationRepository(AppDbContext context) : base(context)
		{
		}

		/// <summary>
		/// Get RealPerson with RealPersonInfo.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<RealPerson?> GetRealPersonWithInfo(Guid id) => await Entities.Where(x => x.Id == id).Include(x => x.RealPersonInfos).AsNoTracking().FirstOrDefaultAsync();

		/// <summary>
		/// کاربر لاگین کرده زنده است؟
		/// </summary>
		/// <returns></returns>
		public async Task<bool> IsUserAlive(Guid requestId) => !await (from e in Entities
																	   join i in Context.RealPersonInfos on e.Id equals i.RealPersonid
																	   where i.IsActive && e.Id == requestId
																	   select i.IsDead).FirstOrDefaultAsync();

		public override Task Update(RealPerson entity, bool save = true)
		{
			Context.Attach(entity).State = EntityState.Modified;

			if (entity.RealPersonInfos != null)
				foreach (var item in entity.RealPersonInfos)
					if (!item.IsActive || item.IsDead)
						Context.Attach(item).State = EntityState.Modified;
					else
						Context.Attach(item).State = EntityState.Added;
			return base.Update(entity, save);
		}

		protected override void SetFieldsForUpdate(RealPerson foundObj, RealPerson data)
		{
			foundObj.Name = data.Name;
			foundObj.Date = data.Date;
			foundObj.LatinFamily = data.LatinFamily;
			foundObj.LatinName = data.LatinName;
			foundObj.FatherName = data.FatherName;
			foundObj.IsMale = data.IsMale;
			foundObj.Family = data.Family;
			foundObj.RealPersonInfos = data.RealPersonInfos;
		}
	}
}