/*using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Requests;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Repository.Requests
{
	/// <summary>
	/// پاراف وافعی
	/// </summary>
	internal sealed class RequestRealSignatureRepository : ApplicationRepository<RequestRealSignature, Guid>, IRequestRealSignatureRepository
	{
		public RequestRealSignatureRepository(AppDbContext context) : base(context)
		{
		}

		public override Task Add(RequestRealSignature entity, bool save = true)
		{
			if (entity.Request.RequestStateLogs != null)
			{
				foreach (var item in entity.Request.RequestStateLogs)
					Context.Attach(item).State = EntityState.Added;
				Context.Attach(entity.Request).State = EntityState.Modified;
			}

			return base.Add(entity, save);
		}

		public override Task Update(RequestRealSignature entity, bool save = true)
		{
			if (entity.Request.RequestStateLogs != null)
			{
				foreach (var item in entity.Request.RequestStateLogs)
					Context.Attach(item).State = EntityState.Added;
				Context.Attach(entity.Request).State = EntityState.Modified;
			}

			return base.Update(entity, save);
		}

		protected override void SetFieldsForUpdate(RequestRealSignature foundObj, RequestRealSignature data)
		{
			foundObj.SignatureFileName = data.SignatureFileName;
			foundObj.ArchiveError = data.ArchiveError;
			foundObj.SignatureArchived = data.SignatureArchived;
		}
	}
}*/