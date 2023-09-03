using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Repository.Requests
{
	internal sealed class RequestRepository : ApplicationRepository<Request, Guid>, IRequestRepository
	{
		public RequestRepository(AppDbContext context) : base(context)
		{
		}

		/// <summary>
		/// تنظیمات فعال درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId">شناسه درخواست</param>
		/// <returns></returns>
		public async Task<AccountTypeSetting> GetAccountTypeSetting(Guid requestId) => await Entities.Where(x => x.Id == requestId)
			.Include(x => x.RequestAccountTypeSetting)
			.ThenInclude(x => x.AccountTypeSetting)
			.Select(x => x.RequestAccountTypeSetting.AccountTypeSetting).FirstOrDefaultAsync() ?? throw StException.DataNotFound("تنظیمات فعال درخواست");

		/// <summary>
		/// Get request with RequestLog and PersonInfo data.
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>Request</returns>
		public async Task<Request> GetWithLogPersonInfo(Guid requestId)
		{
			var res = await (from r in Entities
							 join s in Context.RequestStateLogs on r.Id equals s.RequestId
							 join p in Context.RealPeople on r.PersonId equals p.Id
							 join a in Context.Addresses on p.Id equals a.PersonId
							 join i in Context.RealPersonInfos on p.Id equals i.RealPersonid
							 join j in Context.Jobs on i.JobId equals j.Id
							 join e in Context.Educations on i.EducationId equals e.Id
							 join c in Context.Cities on p.CityId equals c.Id
							 where r.Id == requestId && a.IsActive && i.IsActive && s.RequestState == RequestStateType.Start
							 orderby s.SysDate descending
							 select new { r, s, p, a, i, j, e, c }).FirstOrDefaultAsync();

			if (res == null)
				throw StException.DataNotFound("درخواست");

			return GetRequest(res.r, res.s, res.p, res.a, res.i, res.j, res.e, res.c);
		}

		private static Request GetRequest(Request r, RequestStateLog s, RealPerson p, Address a, RealPersonInfo i, Job j, Education e, City c)
		{
			r.RequestStateLogs = new List<RequestStateLog> { s };
			i.Job = j;
			i.Education = e;
			p.Addresses = new List<Address> { a };
			p.RealPersonInfos = new List<RealPersonInfo> { i };
			r.Person = p;
			p.City = c;
			return r;
		}

		public override Task Update(Request entity, bool save = true)
		{
			if (entity.RequestStateLogs != null)
				foreach (var item in entity.RequestStateLogs)
					Context.Attach(item).State = EntityState.Added;

			return base.Update(entity, save);
		}

		/// <summary>
		/// فقط آخرین وضعیت (مرحله) درخواست تغییر می تواند بکند
		/// </summary>
		/// <param name="foundObj"></param>
		/// <param name="data"></param>
		protected override void SetFieldsForUpdate(Request foundObj, Request data)
		{
			foundObj.RequestStateType = data.RequestStateType;
			foundObj.RequestStateLogs = data.RequestStateLogs;
		}

		/// <summary>
		/// اطلاعات افتتاح حساب کاربر
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public async Task<UserAccount> GetUserAccount(Guid requestId) => await Context.UserAccounts.Where(x => x.Id == requestId).FirstAsync();

		/// <summary>
		/// شماره حساب را بده
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public async Task<string> GetAccountNumber(Guid requestId) => await (from e in Entities
																			 join u in Context.UserAccounts on e.Id equals u.Id
																			 where e.Id == requestId
																			 select u.AccountNumber).FirstOrDefaultAsync() ?? string.Empty;
	}
}