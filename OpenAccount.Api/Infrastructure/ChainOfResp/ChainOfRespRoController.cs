using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Infrastructure.ChainOfResp;
using OpenAccount.Entities.Infrastructure;

namespace OpenAccount.Api.Infrastructure.ChainOfResp
{
	/// <summary>
	/// ReadOnly chained base controller.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TLogic"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TEnum"></typeparam>
	public abstract class ChainOfRespRoController<TEntity, TLogic, TKey, TEnum> : ApplicationRoController<TEntity, TLogic, TKey>
		where TEntity : IBaseEntity<TKey>
		where TLogic : IBaseChainOfRespRoLogic<TEntity, TKey, TEnum>
		where TKey : struct
		where TEnum : Enum
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="accessor">to access header.</param>
		/// <param name="baseLogic"></param>
		/// <param name="controllerEnum"></param>
		/// <param name="ThrowUserData">if UserData.UserId, NationalCode not exists, throw exception</param>
		protected ChainOfRespRoController(IConfiguration configuration, IHttpContextAccessor accessor, TLogic baseLogic, TEnum controllerEnum, bool ThrowUserData = true) :
			base(configuration, accessor, baseLogic, ThrowUserData) => ControllerEnum = controllerEnum;

		/// <summary>
		/// Level of this controller in chain.
		/// </summary>
		protected TEnum ControllerEnum { get; }

		/// <summary>
		/// مرحله بعدی را بر می گرداند
		/// <typeparamref name="TEnum"/>
		/// </summary>
		/// <returns>اگر مرحله ی آخر بود، بعدی نال خواهد بود</returns>
		[HttpGet("GetNextStep")]
		public TEnum? GetNextStep() => ControllerLogic.GetNextStep();

		/// <inheritdoc/>
		public override void ManageChain() => ControllerLogic.ValidateChain();
	}
}