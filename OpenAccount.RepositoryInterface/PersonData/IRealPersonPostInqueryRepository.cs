using OpenAccount.Entities.PersonData;

namespace OpenAccount.RepositoryInterface.PersonData
{
	/// <summary>
	/// اطلاعات پستی
	/// </summary>
	public interface IRealPersonPostInqueryRepository : IBasePersonRepository<RealPerson>
	{
		/// <summary>
		/// Get RealPerson with active RealPersonInfo and active Address.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<RealPerson?> GetRealPersonWithInfoAddress(Guid id);
	}
}