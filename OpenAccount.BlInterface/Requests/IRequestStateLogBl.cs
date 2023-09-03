using OpenAccount.Entities.Requests;
using OpenAccount.BlInterface.Infrastructure;

namespace OpenAccount.BlInterface.Requests
{
	/// <summary>
	/// لاگ تغییر وضعیت درخواست از مرحله ای به مرحله ی دیگر
	/// </summary>
	public interface IRequestStateLogBl : IBaseLogic<RequestStateLog, Guid>
	{
		/// <summary>
		/// لاگ یک درخواست
		/// </summary>
		/// <param name="requestId">شناسه درخواست</param>
		/// <exception cref="StException.DataNotFound()"></exception>
		/// <returns>IEnumerable<RequestStateLog></returns>
		Task<IEnumerable<RequestStateLog>> GetByRequest(Guid requestId);

		/// <summary>
		/// آخرین مرحله ی یک درخواست را که گذرانده
		/// </summary>
		/// <param name="requestId">شناسه درخواست</param>
		/// <returns>RequestStateLog</returns>
		Task<RequestStateLog?> GetLastStateOfRequest(Guid requestId);
	}
}