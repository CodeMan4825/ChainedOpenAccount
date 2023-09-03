using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Requests;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.RepositoryInterface.Requests
{
	/// <summary>
	/// امضای دیجیتال هر درخواست
	/// </summary>
	public interface IRequestDigitalSignatureRepository : IBaseRepository<RequestDigitalSignature, Guid>
	{
		/// <summary>
		/// اطلاعات پرسنلی مورد نیاز امضای دیجیتال
		/// </summary>
		/// <param name="personId"></param>
		/// <returns></returns>
		Task<RealPersonForDSignDto?> GetPersonNeededData(Guid personId);
	}
}