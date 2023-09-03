using OpenAccount.Entities.PersonData;

namespace OpenAccount.BlInterface.PersonData
{
	/// <summary>
	/// اطلاعات شهر - تحصیلات و شغل
	/// </summary>
	public interface IRealPersonInfoCompletionBl : IPersonChainedBl<RealPerson>
	{
		/// <summary>
		/// بروزرسانی اطلاعات شهر - تحصیلات و شغل 
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task PostRealPersonInfoCompletion(RealPersonInfoCompletionDto dto);
	}
}