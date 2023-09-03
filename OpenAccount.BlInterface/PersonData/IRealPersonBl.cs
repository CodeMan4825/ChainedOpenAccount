using OpenAccount.Entities.PersonData;

namespace OpenAccount.BlInterface.PersonData
{
	public interface IRealPersonBl : IBasePersonBl<RealPerson> 
	{
		/// <summary>
		/// کاربر لاگین کرده زنده است؟
		/// </summary>
		/// <returns></returns>
		Task<bool> IsUserAlive(Guid requestid);
	}
}