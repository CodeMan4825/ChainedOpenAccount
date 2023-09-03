using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Requests;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Repository.Requests
{
	/// <summary>
	/// درخواست اولیه ی افتتاح حساب
	/// </summary>
	internal sealed class RequestStartRepository : ApplicationRepository<Request, Guid>, IRequestStartRepository
	{
		public RequestStartRepository(AppDbContext context) : base(context)
		{
		}

		public override Task Add(Request entity, bool save = true)
		{
			foreach (var item in entity.RequestStateLogs)
				Context.Attach(item).State = EntityState.Added;
			
			if (entity.RequestAccountTypeSetting != null)
				Context.Attach(entity.RequestAccountTypeSetting).State = EntityState.Added;

			if (entity.Person != null)
				Context.Attach(entity.Person).State = EntityState.Added;

			if (entity.Person != null && entity.Person.Addresses != null)
				foreach (var address in entity.Person.Addresses)
					Context.Attach(address).State = EntityState.Added;

			return base.Add(entity, save);
		}
	}
}