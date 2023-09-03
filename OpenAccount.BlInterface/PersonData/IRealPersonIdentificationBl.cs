using OpenAccount.Entities.PersonData;
using OpenAccount.Publics;

namespace OpenAccount.BlInterface.PersonData
{
	/// <summary>
	/// اطلاعات هویتی
	/// </summary>
	public interface IRealPersonIdentificationBl : IPersonChainedBl<RealPerson>
	{
		//Task<object> GetProfile(UserInfoResponseDto data);

		/// <summary>
		/// استعلام ثبت احوال
		/// Offline
		/// </summary>
		Task IdentityInquiry();

		/// <summary>
		/// آیا کاربر زنده است؟
		/// </summary>
		/// <returns></returns>
		/// <exception cref="StException.AccessDenied">دسترسی غیرمجاز - کاربر زنده نیست</exception>
		/// <exception cref="StException.ServiceUnavailable">احراز هویت</exception>
		/// <exception cref="StException.DataNotFound">اطلاعات پرسنلی</exception>
		/// <exception cref="StException.ResultNotAcceptable">service ActionCode</exception>
		Task IsUserAlive();
	}
}