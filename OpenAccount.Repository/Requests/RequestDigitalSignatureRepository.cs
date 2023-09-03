using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Requests;
using OpenAccount.Repository.Infrastructure;
using OpenAccount.RepositoryInterface.Requests;
using System.Security.Cryptography.X509Certificates;

namespace OpenAccount.Repository.Requests
{
	/// <summary>
	/// امضای دیجیتال هر درخواست
	/// </summary>
	internal sealed class RequestDigitalSignatureRepository : ApplicationRepository<RequestDigitalSignature, Guid>, IRequestDigitalSignatureRepository
	{
		public RequestDigitalSignatureRepository(AppDbContext context) : base(context)
		{
		}

		/// <summary>
		/// اطلاعات پرسنلی مورد نیاز امضای دیجیتال
		/// </summary>
		/// <param name="personId"></param>
		/// <returns></returns>
		public async Task<RealPersonForDSignDto?> GetPersonNeededData(Guid personId) => await Context.RealPeople.Where(x => x.Id == personId).Select(x => new RealPersonForDSignDto
		{
			Family = x.Family,
			Name = x.Name,
			NationalCode = x.NationalCode,
			BirthDate = x.Date
		}).FirstOrDefaultAsync();

		public override Task Update(RequestDigitalSignature entity, bool save = true)
		{
			if (entity.RequestDigitalSignatureLogs != null)
				foreach (var item in entity.RequestDigitalSignatureLogs)
					Context.Attach(item).State = EntityState.Added;

			if (entity.PdfSignedByBank && !string.IsNullOrEmpty(entity.FileNameInDms))
				if (entity.Request.RequestStateLogs != null && entity.Request.RequestStateLogs.Any())
				{
					foreach (var item in entity.Request.RequestStateLogs)
						Context.Attach(item).State = EntityState.Added;
					Context.Attach(entity.Request).State = EntityState.Modified;
				}
			return base.Update(entity, save);
		}

		protected override void SetFieldsForUpdate(RequestDigitalSignature foundObj, RequestDigitalSignature data)
		{
			foundObj.FirstDigest = data.FirstDigest;
			foundObj.SysDate = data.SysDate;
			foundObj.RootCertification = data.RootCertification;
			foundObj.PdfGenerated = data.PdfGenerated;
			foundObj.TempPdfFile = data.TempPdfFile;

			foundObj.SignGeneratedByApp = data.SignGeneratedByApp;
			foundObj.FinalDigest = data.FinalDigest;

			foundObj.PdfSignedByBank = data.PdfSignedByBank;
			foundObj.FileNameInDms = data.FileNameInDms;
		}
	}
}