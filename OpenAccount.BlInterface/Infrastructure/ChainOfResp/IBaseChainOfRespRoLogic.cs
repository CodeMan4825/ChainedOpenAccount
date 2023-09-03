using OpenAccount.Entities.Infrastructure;

namespace OpenAccount.BlInterface.Infrastructure.ChainOfResp
{
	/// <summary>
	/// Base of ChainOfResp for readonly bl.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TEnum"></typeparam>
	public interface IBaseChainOfRespRoLogic<TEntity, in TKey, TEnum> : IBaseRoLogic<TEntity, TKey>, IBaseChainOfRespBl<TEnum>
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
		where TEnum : Enum
	{
		/// <summary>
		/// مرحله بعدی را بر می گرداند
		/// مرحله ی آخر باید پایان باشد
		/// <typeparamref name="TEnum"/>
		/// </summary>
		TEnum GetNextStep();
	}
}