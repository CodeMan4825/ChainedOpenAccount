using OpenAccount.Entities.Requests;

namespace OpenAccount.BlInterface.Requests
{
	/// <summary>
	/// ارسال پاراف خیس به بانک
	/// </summary>
	public interface IRequestRealSignatureToBankBl : IOpenAccountChainedBl<RequestRealSignatureToBank, Guid>
	{
		/// <summary>
		/// ارسال امضای خیس به بانک
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		Task SendToBank(Guid requestId);
	}
}