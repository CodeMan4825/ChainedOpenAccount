using OpenAccount.Entities.PersonData;

namespace OpenAccount.RepositoryInterface.PersonData
{
	/// <summary>
	/// اشخاص حقیقی
	/// </summary>
	public interface IRealPersonIdentificationRepository : IBasePersonRepository<RealPerson> 
	{
		/// <summary>
		/// Get RealPerson with RealPersonInfo.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<RealPerson?> GetRealPersonWithInfo(Guid id);

		/// <summary>
		/// کاربر لاگین کرده زنده است؟
		/// </summary>
		/// <returns></returns>
		Task<bool> IsUserAlive(Guid requestId);
	}
}