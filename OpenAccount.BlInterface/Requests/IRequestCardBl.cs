using OpenAccount.Entities.Requests;

namespace OpenAccount.BlInterface.Requests
{
	/// <summary>
	/// سفارش کارت
	/// </summary>
	public interface IRequestCardBl : IOpenAccountChainedBl<RequestCard, Guid>
	{
		/// <summary>
		/// لیست کارت های موجود را از سرویس کارت برگردان
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<RequestCard>> GetCardsFromService();
	}
}