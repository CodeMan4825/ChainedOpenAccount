using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Publics;

namespace OpenAccount.BlInterface.PersonData
{
	/// <summary>
	/// اطلاعات پستی
	/// </summary>
	public interface IRealPersonPostInqueryBl : IPersonChainedBl<RealPerson>
	{
		/// <summary>
		/// استعلام کدپستی
		/// </summary>
		/// <param name="postalCode"></param>
		/// <returns></returns>
		Task<AddressToConfirmPostCodeDto> PostInquiry(string postalCode);

		/// <summary>
		/// استعلام کدپستی بصورت آفلاین
		/// </summary>
		/// <param name="postalCode">کد پستی</param>
		/// <returns></returns>
		Task<AddressToConfirmPostCodeDto> PostalCodeOfflineInquiry();
	}
}