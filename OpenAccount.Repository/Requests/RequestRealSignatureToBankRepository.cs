using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Requests;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Repository.Requests
{
	/// <summary>
	/// ارسال پاراف خیس به بانک
	/// </summary>
	internal sealed class RequestRealSignatureToBankRepository : ApplicationRepository<RequestRealSignatureToBank, Guid>, IRequestRealSignatureToBankRepository
	{
		public RequestRealSignatureToBankRepository(AppDbContext context) : base(context)
		{
		}

		public override Task Add(RequestRealSignatureToBank entity, bool save = true)
		{
			if (entity.Request.RequestStateLogs != null && entity.Request.RequestStateLogs.Any())
			{
				foreach (var item in entity.Request.RequestStateLogs)
					Context.Attach(item).State = EntityState.Added;
				Context.Attach(entity.Request).State = EntityState.Modified;
			}
			return base.Add(entity, save);
		}

		public override Task Update(RequestRealSignatureToBank entity, bool save = true)
		{
			if (entity.Request.RequestStateLogs != null && entity.Request.RequestStateLogs.Any())
			{
				foreach (var item in entity.Request.RequestStateLogs)
					Context.Attach(item).State = EntityState.Added;
				Context.Attach(entity.Request).State = EntityState.Modified;
			}
			return base.Update(entity, save);
		}

		protected override void SetFieldsForUpdate(RequestRealSignatureToBank foundObj, RequestRealSignatureToBank data)
		{
			foundObj.SendToBankMessage = data.SendToBankMessage;
			foundObj.SignatureSentToBank = data.SignatureSentToBank;
		}
	}
}