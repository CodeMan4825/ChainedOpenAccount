using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Requests.InqueryCheque;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Repository.Requests
{
	/// <summary>
	/// استعلام چک
	/// </summary>
	internal sealed class RequestChequeInqueryRepository : ApplicationRepository<SamatChequeInquiryRequest, Guid>, IRequestChequeInqueryRepository
	{
		public RequestChequeInqueryRepository(AppDbContext context) : base(context)
		{
		}

		public override Task Add(SamatChequeInquiryRequest entity, bool save = true)
		{
			foreach (var item in entity.SamatBouncedChequeItems)
			{
				Context.Attach(item).State = EntityState.Added;
				foreach (var rsn in item.BouncedReasons)
					Context.Attach(rsn).State = EntityState.Added;
			}
			if (entity.Request.RequestStateLogs != null && entity.Request.RequestStateLogs.Any())
			{
				Context.Attach(entity.Request).State = EntityState.Modified;
				Context.Attach(entity.Request.RequestStateLogs.OrderByDescending(x => x.SysDate).First()).State = EntityState.Added;
			}
			return base.Add(entity, save);
		}

		/// <summary>
		/// آخرین استعلام چک یک درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>SamatChequeInquiryRequest</returns>
		public async Task<SamatChequeInquiryRequest?> GetLastInquiry(Guid requestId) =>
			await Entities.Include(x => x.SamatBouncedChequeItems).ThenInclude(x => x.BouncedReasons).AsNoTracking().Where(x => x.RequestId == requestId).OrderByDescending(x => x.SysDate).FirstOrDefaultAsync();
	}
}