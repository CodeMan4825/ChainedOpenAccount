using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Requests.InqueryLoan;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Repository.Requests
{
	/// <summary>
	/// استعلام تسهیلات
	/// </summary>
	internal sealed class RequestFacilityInqueryRepository : ApplicationRepository<SamatLoanInquiryRequest, Guid>, IRequestFacilityInqueryRepository
	{
		public RequestFacilityInqueryRepository(AppDbContext context) : base(context)
		{
		}

		public override Task Add(SamatLoanInquiryRequest entity, bool save = true)
		{
			foreach (var item in entity.EstelamAsliRows)
				Context.Attach(item).State = EntityState.Added;

			if (entity.Request.RequestStateLogs != null && entity.Request.RequestStateLogs.Any())
			{
				Context.Attach(entity.Request).State = EntityState.Modified;
				Context.Attach(entity.Request.RequestStateLogs.OrderByDescending(x => x.SysDate).First()).State = EntityState.Added;
			}
			return base.Add(entity, save);
		}

		/// <summary>
		/// آخرین استعلام تسهیلات یک درخواست را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>SamatLoanInquiryRequest</returns>
		public async Task<SamatLoanInquiryRequest?> GetLastInquiry(Guid requestId) => 
			await Entities.Include(x => x.EstelamAsliRows).AsNoTracking().Where(x => x.RequestId == requestId).OrderByDescending(x => x.SysDate).FirstOrDefaultAsync();
	}
}