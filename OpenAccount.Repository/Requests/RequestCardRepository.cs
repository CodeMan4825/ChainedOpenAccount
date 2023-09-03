using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Requests;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Repository.Requests
{
	/// <summary>
	/// سفارش کارت
	/// </summary>
	internal sealed class RequestCardRepository : ApplicationRepository<RequestCard, Guid>, IRequestCardRepository
	{
		public RequestCardRepository(AppDbContext context) : base(context)
		{
		}

		public override Task Add(RequestCard entity, bool save = true)
		{
			if (entity.Request.RequestStateLogs != null && entity.Request.RequestStateLogs.Any())
			{
				Context.Attach(entity.Request).State = EntityState.Modified;
				Context.Attach(entity.Request.RequestStateLogs.OrderByDescending(x => x.SysDate).First()).State = EntityState.Added;
			}
			return base.Add(entity, save);
		}

		public override Task Update(RequestCard entity, bool save = true)
		{
			Context.Attach(entity.Request).State = EntityState.Modified;
			Context.Attach(entity.Request.RequestStateLogs.OrderByDescending(x => x.SysDate).First()).State = EntityState.Added;

			return base.Update(entity, save);
		}

		protected override void SetFieldsForUpdate(RequestCard foundObj, RequestCard data)
		{
			foundObj.IsActive = data.IsActive;
			foundObj.CardPro = data.CardPro;
			foundObj.Description = data.Description;
			foundObj.KeyCode = data.KeyCode;
			foundObj.Layout = data.Layout;
			foundObj.TemplateName = data.TemplateName;
			foundObj.TName = data.TName;
		}
	}
}