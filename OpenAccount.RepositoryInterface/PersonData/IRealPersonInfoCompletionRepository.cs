using OpenAccount.Entities.PersonData;

namespace OpenAccount.RepositoryInterface.PersonData
{
	/// <summary>
	/// اطلاعات تکمیلی
	/// </summary>
	public interface IRealPersonInfoCompletionRepository : IBasePersonRepository<RealPerson>
	{
		/// <summary>
		/// Get RealPerson with active RealPersonInfo.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<RealPerson?> GetRealPersonWithInfo(Guid id);
	}
}