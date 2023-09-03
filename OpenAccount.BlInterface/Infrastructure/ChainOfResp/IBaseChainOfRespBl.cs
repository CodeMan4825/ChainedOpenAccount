namespace OpenAccount.BlInterface.Infrastructure.ChainOfResp
{
	/// <summary>
	/// Base of just ChainOfResp.
	/// </summary>
	/// <typeparam name="TEnum"></typeparam>
	public interface IBaseChainOfRespBl<TEnum>
		where TEnum : Enum
	{
		/// <summary>
		/// اعتباراین مرحله کنترل می شود
		/// </summary>
		/// <returns>exception if false</returns>
		void Validate();

		/// <summary>
		/// Calls await Validate();
		/// customiza all validates with this method.
		/// </summary>
		/// <returns>exception if false</returns>
		void ValidateChain();
	}
}