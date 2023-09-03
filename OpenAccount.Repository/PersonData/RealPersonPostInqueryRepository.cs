using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Publics;
using OpenAccount.Publics;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Repository.PersonData
{
	/// <summary>
	/// اطلاعات پستی
	/// </summary>
	internal sealed class RealPersonPostInqueryRepository : BasePersonRepository<RealPerson>, IRealPersonPostInqueryRepository
	{
		public RealPersonPostInqueryRepository(AppDbContext context) : base(context)
		{
		}

		/// <summary>
		/// Get RealPerson with active RealPersonInfo and active Address.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<RealPerson?> GetRealPersonWithInfoAddress(Guid id)
		{
			var info = await Context.RealPersonInfos.Where(x => x.RealPersonid == id && x.IsActive).AsNoTracking().FirstOrDefaultAsync();
			var result = await Entities.Where(x => x.Id == id).Include(x => x.Addresses).AsNoTracking().FirstOrDefaultAsync();
			if (result != null && result.Addresses != null)
			{
				var address = new List<Address>();
				foreach (var item in result.Addresses)
					if (item.IsActive)
					{
						address.Add(item);
						break;
					}
				result.Addresses = address;
			}
			if (info != null && result != null)
				result.RealPersonInfos = new List<RealPersonInfo> { info };
			else
				return null;

			return result;
		}

		public override Task Update(RealPerson entity, bool save = true)
		{
			if (entity.RealPersonInfos == null || !entity.RealPersonInfos.Any())
				throw StException.DataNotFound("اطلاعات تکمیلی شخص");

			var info = entity.RealPersonInfos.First();
			Context.Attach(info).State = EntityState.Modified;

			if (entity.Addresses != null)
				foreach (var add in entity.Addresses)
					if (add.IsActive)
						Context.Attach(add).State = EntityState.Added;
					else
						Context.Attach(add).State = EntityState.Modified;

			return base.Update(entity, save);
		}

		protected override void SetFieldsForUpdate(RealPerson foundObj, RealPerson data)
		{ //nothing will update
		}
	}
}